using NoteTakingApp.Core.Interfaces;
using NoteTakingApp.Models.Entities;
using NoteTakingApp.Services.Interfaces;
using SQLite;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteTakingApp.Services
{
    public class DataStoreService<TEntity> : IDataStoreService<TEntity> where TEntity : IEntityRecord, new()
    {
        protected readonly SQLiteAsyncConnection Connection;

        public DataStoreService()
        {
            Connection = DependencyService.Get<ISQLite>().GetAsyncConnection();
            Connection.CreateTableAsync<TEntity>();
        }

        public AsyncTableQuery<TEntity> Query => Connection.Table<TEntity>();

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await Connection.GetAsync<TEntity>(id);
        }

        public virtual async Task InsertAsync(TEntity record)
        {
            await Connection.InsertAsync(record);
        }

        public virtual async Task UpdateAsync(TEntity record)
        {
            await Connection.UpdateAsync(record);
        }

        public virtual async Task DeleteAsync(TEntity record)
        {
            await Connection.DeleteAsync(record);
        }

        public virtual async Task DeleteAllAsync()
        {
            await Connection.DropTableAsync<TEntity>();
            await Connection.CreateTableAsync<TEntity>();
        }
    }
}
