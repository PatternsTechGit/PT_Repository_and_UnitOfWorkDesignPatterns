using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity t);
        Task<TEntity[]> BatchAddAsync(TEntity[] entities);
        Task<int> CountAsync();
        void DeleteAsync(TEntity t);
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, string[] includes = null);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetAsync(string id, string include = null);
        Task<TEntity> GetAsync(string id, string[] includes = null);
        Task<TEntity> GetAsync(string id);
        Task<TEntity> UpdateAsync(TEntity updated);
        Task<bool> Exists(Expression<Func<TEntity, bool>> match);
    }
}
