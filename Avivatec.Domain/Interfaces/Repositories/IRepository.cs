using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avivatec.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IList<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task<int> Count();

        Task<IList<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where);

        Task<IList<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] includes);

        Task<IList<TEntity>> CustomFind(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> orderby,
            params Expression<Func<TEntity, object>>[] includes);

        void Save(TEntity entity);

        void SaveMany(IEnumerable<TEntity> entity);

        void Delete(TEntity entity);

        void DeleteMany(IEnumerable<TEntity> entity);
    }
}
