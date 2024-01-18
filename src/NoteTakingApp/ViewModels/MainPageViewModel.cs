using AutoMapper;
using NoteTakingApp.Constants;
using NoteTakingApp.Core;
using NoteTakingApp.Models;
using NoteTakingApp.Services.Interfaces;
using NoteTakingApp.ViewModels.Commands;
using NoteTakingApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NoteTakingApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Fields

        private readonly IMainPageService _mainPageService;
        private readonly IDataStoreService<Models.Entities.NoteModel> _dataStoreService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public MainPageViewModel(
            IMainPageService mainPageService,
            IDataStoreService<Models.Entities.NoteModel> dataStoreService,
            IMapper mapper)
        {
            _mainPageService = mainPageService;
            _dataStoreService = dataStoreService;
            _mapper = mapper;

            PageTitle = (string)App.Current.Resources["AppNameString"];
        }

        #endregion

        #region Public Methods

        public async Task Init()
        {
            if (Notes != null)
            {
                await LoadLocalData();
                RaisePropertyChanged(nameof(HasNotes));
                return;
            }
            await SyncAndRefreshData();
        }

        #endregion

        #region Private Methods

        private async Task SelectItem(object obj)
        {
            if (obj == null)
                return;

            await SetInputBusyAsync(async () =>
            {
                try
                {
                    var args = (ItemTappedEventArgs)obj;
                    await SetBusyAsync(async () =>
                    {
                        var page = new DetailsPage();
                        var selectedItem = (NoteModel)args.Item;
                        var viewModel = (DetailsPageViewModel)page.BindingContext;
                        viewModel.Note = _mapper.Map<Models.Entities.NoteModel>(selectedItem);
                        await Navigation.PushAsync(page);
                    });
                }
                catch (Exception ex)
                {
                    ExceptionHandler.LogException(ex);
                }
            });
        }

        private async Task NavigateToAddEditPage()
        {
            var page = new AddEditPage();
            await Navigation.PushAsync(page);
        }

        private async Task SyncOnlineData()
        {
            await SetBusyAsync(async () =>
            {
                try
                {
                    var noteEntities = await _dataStoreService.Query.ToListAsync();
                    var noteListModel = new NoteListModel
                    {
                        Notes = _mapper.Map<List<NoteModel>>(noteEntities),
                    };
                    var noteDtos = _mapper.Map<Models.Dtos.NoteListModel>(noteListModel);
                    await _mainPageService.SyncNotes(noteDtos);

                    var noteList = await _mainPageService.GetNotes();
                    await _dataStoreService.UpdateAsync(_mapper.Map<Models.Entities.NoteModel>(noteList.Notes));
                    Notes = noteList.Notes;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.LogException(ex);
                }
            }, 0, "Syncing...");
        }

        private async Task LoadLocalData()
        {
            await SetBusyAsync(async () =>
            {
                try
                {
                    var noteEntities = await _dataStoreService.Query.ToListAsync();
                    Notes = _mapper.Map<List<NoteModel>>(noteEntities);

                    GroupedData = Notes.OrderByDescending(x => x.IsFavourite)
                        .ThenByDescending(x => x.DateLastUpdated)
                        .GroupBy(x => x.IsFavourite ? AppGlobal.FavouritesText : x.DateLastUpdated.ToString("ddd, dd MMMM yyyy"))
                        .Select(x => new ObservableGroupCollection<string, NoteModel>(x))
                        .ToList();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.LogException(ex);
                }
            }, 0, "Loading...");
        }

        private async Task SyncAndRefreshData()
        {
            bool isOnline = AppGlobal.IsOnline;
            if (isOnline)
            {
                // Sync
                await SyncOnlineData();
            }
            else
            {
                await Page.DisplayAlert("No Connection Available", "You are currently not connected to the internet. Please connect to sync online data.", "OK");
            }
            await LoadLocalData();
            RaisePropertyChanged(nameof(HasNotes));
        }

        #endregion

        #region Commands

        private ICommand _selectItemCommand;
        public ICommand SelectItemCommand
        {
            get { return _selectItemCommand ??= new DelegateCommand(async (obj) => await SelectItem(obj)); }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ??= new DelegateCommand(async (obj) => await NavigateToAddEditPage()); }
        }

        private ICommand _syncCommand;
        public ICommand SyncCommand
        {
            get { return _syncCommand ??= new DelegateCommand(async (obj) => await SyncAndRefreshData()); }
        }

        #endregion

        #region Properties

        private List<ObservableGroupCollection<string, NoteModel>> _groupedData;
        public List<ObservableGroupCollection<string, NoteModel>> GroupedData
        {
            get => _groupedData;
            set => SetProperty(ref _groupedData, value, nameof(GroupedData));
        }

        private List<NoteModel> _notes;
        public List<NoteModel> Notes
        {
            get => _notes;
            set
            {
                SetProperty(ref _notes, value, nameof(Notes));
                RaisePropertyChanged(nameof(HasNotes));
            }
        }

        public bool HasNotes { get => !IsBusy && Notes?.Count > 0; }

        #endregion
    }
}
