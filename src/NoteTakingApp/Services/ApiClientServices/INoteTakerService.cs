using NoteTakingApp.Models.Dtos;
using Refit;
using System.Threading.Tasks;

namespace NoteTakingApp.Services.ApiClientServices
{
    [Headers("Content-Type: application/json")]
    public interface INoteTakerService
    {
        [Get("/notes?token={token}&page={page}")]
        Task<NoteListModel> GetNotes([AliasAs("token")] string token, [AliasAs("page")] int page);

        [Post("/syncnotes")]
        Task<bool> SyncNotes([Body] NoteListModel noteList, [Header("Authorization")] string token);
    }
}
