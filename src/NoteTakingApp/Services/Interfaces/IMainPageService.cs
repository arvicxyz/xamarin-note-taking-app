using System.Threading.Tasks;

namespace NoteTakingApp.Services.Interfaces
{
    public interface IMainPageService
    {
        Task<Models.NoteListModel> GetNotes();
        Task<bool> SyncNotes(Models.Dtos.NoteListModel noteList);
    }
}
