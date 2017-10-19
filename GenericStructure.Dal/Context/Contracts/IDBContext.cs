using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Context.Contracts
{
    public interface IDbContext
    {
        Database Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry(object entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
