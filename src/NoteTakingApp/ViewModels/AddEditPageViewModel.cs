using AutoMapper;
using NoteTakingApp.Core;
using NoteTakingApp.Models.Entities;
using NoteTakingApp.Services.Interfaces;
using NoteTakingApp.Utilities;
using NoteTakingApp.ViewModels.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteTakingApp.ViewModels
{
    public class AddEditPageViewModel : BaseViewModel
    {
        #region Fields

        private readonly string EmptyTitleError = "ERROR: Please enter a Title to your note";

        private readonly IDataStoreService<NoteModel> _dataStoreService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public AddEditPageViewModel(
            IDataStoreService<NoteModel> dataStoreService,
            IMapper mapper)
        {
            _dataStoreService = dataStoreService;
            _mapper = mapper;

            _note = null;
        }

        #endregion

        #region Public Methods

        public void Init()
        {
            if (IsEdit)
            {
                PageTitle = "Edit Your Note";
                PageSubtitle = string.Empty;
                NoteTitle = Note.Title;
                NoteDescription = Note.Description;
            }
            else
            {
                PageTitle = "Add Your Note";
                PageSubtitle = "Enter a Title and Description";
            }
        }

        public async Task<bool> BackNav()
        {
            try
            {
                bool hasEdits = false;
                if (IsEdit)
                {
                    hasEdits = !Note.Title.Equals(NoteTitle) || !Note.Description.Equals(NoteDescription);
                }
                else
                {
                    hasEdits = !string.IsNullOrEmpty(NoteTitle) || !string.IsNullOrEmpty(NoteDescription);
                }

                if (hasEdits)
                {
                    var willDiscard = await DialogPopup.Show("Discard?", "You have unsaved changes to this note. Do you want to discard this note?", "Yes", "No");
                    if (!willDiscard)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
            }

            await Navigation.PopAsync();

            return true;
        }

        #endregion

        #region Private Methods

        private async Task SaveNote()
        {
            ErrorText = string.Empty;
            if (string.IsNullOrEmpty(NoteTitle))
            {
                ErrorText = EmptyTitleError;
                return;
            }

            await SetBusyAsync(async () =>
            {
                try
                {
                    var noteEntity = new NoteModel
                    {
                        Title = NoteTitle,
                        Description = NoteDescription,
                        DateCreated = DateTime.Now,
                        DateLastUpdated = DateTime.Now
                    };
                    await _dataStoreService.InsertAsync(noteEntity);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.LogException(ex);
                }
            }, 0, "Saving...");

            await Navigation.PopAsync();
        }

        private async Task UpdateNote()
        {
            ErrorText = string.Empty;
            if (string.IsNullOrEmpty(NoteTitle))
            {
                ErrorText = EmptyTitleError;
                return;
            }

            await SetBusyAsync(async () =>
            {
                try
                {
                    var noteToUpdate = await _dataStoreService.GetByIdAsync(Note.Id);
                    noteToUpdate.Title = NoteTitle;
                    noteToUpdate.Description = NoteDescription;
                    noteToUpdate.DateLastUpdated = DateTime.Now;
                    await _dataStoreService.UpdateAsync(noteToUpdate);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.LogException(ex);
                }
            }, 0, "Updating...");

            await Navigation.PopAsync();
        }

        #endregion

        #region Commands

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand ??= new DelegateCommand(async (obj) => await BackNav()); }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand ??= new DelegateCommand(async (obj) => await SaveNote()); }
        }

        private ICommand _updateCommand;
        public ICommand UpdateCommand
        {
            get { return _updateCommand ??= new DelegateCommand(async (obj) => await UpdateNote()); }
        }

        #endregion

        #region Properties

        public bool IsEdit
        {
            get => Note != null;
        }

        private NoteModel _note;
        public NoteModel Note
        {
            get => _note;
            set
            {
                SetProperty(ref _note, value, nameof(Note));
                RaisePropertyChanged(nameof(IsEdit));
            }
        }

        private string _noteTitle;
        public string NoteTitle
        {
            get => _noteTitle;
            set => SetProperty(ref _noteTitle, value, nameof(NoteTitle));
        }

        private string _noteDescription;
        public string NoteDescription
        {
            get => _noteDescription;
            set => SetProperty(ref _noteDescription, value, nameof(NoteDescription));
        }

        private string _errorText;
        public string ErrorText
        {
            get => _errorText;
            set => SetProperty(ref _errorText, value, nameof(ErrorText));
        }

        #endregion
    }
}
