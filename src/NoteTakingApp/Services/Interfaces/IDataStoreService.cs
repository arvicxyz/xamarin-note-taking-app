using NoteTakingApp.Models.Entities;
using SQLite;
using System.Threading.Tasks;

namespace NoteTakingApp.Services.Interfaces
{
    public interface IDataStoreService<TEntity> where TEntity : IEntityRecord, new()
    {
        AsyncTableQuery<TEntity> Query { get; }
        Task<TEntity> GetByIdAsync(int id);
        Task InsertAsync(TEntity record);
        Task UpdateAsync(TEntity record);
        Task DeleteAsync(TEntity record);
        Task DeleteAllAsync();
    }
}
