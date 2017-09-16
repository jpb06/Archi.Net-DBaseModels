using GenericStructure.DataAccessLayer.Context.Contracts;
using GenericStructure.DataAccessLayer.Manipulation.Repositories.Contracts;
using GenericStructure.DataAccessLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.DataAccessLayer.Manipulation.Repositories.Base
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        internal IDBContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository()
        {
            this.context = null;
            this.dbSet = null;
        }

        public GenericRepository(IDBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public TEntity GetByID(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entityToDelete)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
