using AutoMapper;
using NoteTakingApp.Core;
using NoteTakingApp.Models.Entities;
using NoteTakingApp.Services.Interfaces;
using NoteTakingApp.Utilities;
using NoteTakingApp.ViewModels.Commands;
using NoteTakingApp.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NoteTakingApp.ViewModels
{
    public class DetailsPageViewModel : BaseViewModel
    {
        #region Fields

        private readonly Color DefaultColor = (Color)App.Current.Resources["IconsColor"];
        private readonly Color FavouritesColor = (Color)App.Current.Resources["FavouritesColor"];

        private readonly IDataStoreService<NoteModel> _dataStoreService;
        private readonly IMapper _mapper;

        private bool _hasChanges;

        #endregion

        #region Constructors

        public DetailsPageViewModel(
            IDataStoreService<NoteModel> dataStoreService,
            IMapper mapper)
        {
            _dataStoreService = dataStoreService;
            _mapper = mapper;

            _hasChanges = false;
        }

        #endregion

        #region Public Methods

        public async Task Init()
        {
            try
            {
                Note = await _dataStoreService.GetByIdAsync(Note.Id);
                Note.Id = Note.LocalId > 0 ? Note.LocalId : Note.Id;
                UpdateFavourite();
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
            }
        }

        #endregion

        #region Private Methods

        private async Task BackNav()
        {
            await Navigation.PopAsync();
        }

        private async Task NavigateToAddEditPage()
        {
            try
            {
                var page = new AddEditPage();
                var viewModel = (AddEditPageViewModel)page.BindingContext;
                viewModel.Note = Note;
                await Navigation.PushAsync(page);
                _hasChanges = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
            }
        }

        private async Task DeleteNote()
        {
            var willDelete = await DialogPopup.Show("Delete?", "Are you sure you want to delete this note?", "Yes", "No");
            if (!willDelete)
            {
                return;
            }

            await SetBusyAsync(async () =>
            {
                try
                {
                    var noteToDelete = await _dataStoreService.GetByIdAsync(Note.Id);
                    await _dataStoreService.DeleteAsync(noteToDelete);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.LogException(ex);
                }
            }, 0, "Deleting...");

            await Navigation.PopAsync();
        }

        private async Task AddToFavourites()
        {
            try
            {
                Note.IsFavourite = !Note.IsFavourite;
                UpdateFavourite();
                await _dataStoreService.UpdateAsync(Note);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
            }
        }

        private void UpdateFavourite()
        {
            if (Note.IsFavourite)
                FavouriteIconColor = FavouritesColor;
            else
                FavouriteIconColor = DefaultColor;
        }

        #endregion

        #region Commands

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand ??= new DelegateCommand(async (obj) => await BackNav()); }
        }

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get { return _editCommand ??= new DelegateCommand(async (obj) => await NavigateToAddEditPage()); }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ??= new DelegateCommand(async (obj) => await DeleteNote()); }
        }

        private ICommand _favouriteCommand;
        public ICommand FavouriteCommand
        {
            get { return _favouriteCommand ??= new DelegateCommand(async (obj) => await AddToFavourites()); }
        }

        #endregion

        #region Properties

        private NoteModel _note;
        public NoteModel Note
        {
            get => _note;
            set => SetProperty(ref _note, value, nameof(Note));
        }

        private Color _favouriteIconColor;
        public Color FavouriteIconColor
        {
            get => _favouriteIconColor;
            set => SetProperty(ref _favouriteIconColor, value, nameof(FavouriteIconColor));
        }

        #endregion
    }
}
