using AutoMapper;
using NoteTakingApp.Constants;
using NoteTakingApp.Core;
using NoteTakingApp.Models;
using NoteTakingApp.Services.ApiClientServices;
using NoteTakingApp.Services.Interfaces;
using System.Threading.Tasks;

namespace NoteTakingApp.Services
{
    public class MainPageService : BaseService, IMainPageService
    {
        private readonly IApiService<INoteTakerService> _noteTakerService;
        private readonly IMapper _mapper;

        public MainPageService(
            IApiService<INoteTakerService> noteTakerService,
            IMapper mapper)
        {
            _noteTakerService = noteTakerService;
            _mapper = mapper;
        }

        public async Task<NoteListModel> GetNotes()
        {
            var response = await InvokeWithPolicyAsync(() => _noteTakerService.Api.GetNotes(AppConstants.ApiToken, 1));

            if (response.FinalException != null)
            {
                ExceptionHandler.LogException(response.FinalException);
                throw response.FinalException;
            }

            return _mapper.Map<NoteListModel>(response.Result);
        }

        public async Task<bool> SyncNotes(Models.Dtos.NoteListModel noteList)
        {
            var response = await InvokeWithPolicyAsync(() => _noteTakerService.Api.SyncNotes(noteList, AppConstants.ApiToken));

            if (response.FinalException != null)
            {
                ExceptionHandler.LogException(response.FinalException);
                throw response.FinalException;
            }

            return response.Result;
        }
    }
}
