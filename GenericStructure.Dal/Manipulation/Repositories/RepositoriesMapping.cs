using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Contracts;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Base;
using GenericStructure.Dal.Manipulation.Repositories.Implementation.Specific;
using GenericStructure.Dal.Models;
using GenericStructure.Dal.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Manipulation.Repositories
{
    internal class RepositoriesMapping
    {
        public readonly Dictionary<Type, Type> modelTypesSpecificTypesMapping;

        public readonly Dictionary<Type, Func<object>> genericFactories;
        public readonly Dictionary<Type, Func<object>> specificFactories;

        #region Mapping Definition
        public RepositoriesMapping(IDBContext context)
        {
            this.modelTypesSpecificTypesMapping = new Dictionary<Type, Type>
            {
                { typeof(Article), typeof(IArticlesRepository) },
                { typeof(Category), typeof(ICategoriesRepository) },
                { typeof(Customer), typeof(ICustomersRepository) },
                { typeof(Order), typeof(IOrdersRepository) },
                { typeof(OrderDetail), typeof(IOrderDetailsRepository) }
            };

            this.genericFactories = new Dictionary<Type, Func<object>>
            {
                { typeof(IGenericRepository<Article>), () => new GenericRepository<Article>(context) },
                { typeof(IGenericRepository<Category>), () => new GenericRepository<Category>(context) },
                { typeof(IGenericRepository<Customer>), () => new GenericRepository<Customer>(context) },
                { typeof(IGenericRepository<Order>), () => new GenericRepository<Order>(context) },
                { typeof(IGenericRepository<OrderDetail>), () => new GenericRepository<OrderDetail>(context) },
            };

            this.specificFactories = new Dictionary<Type, Func<object>>
            {
                { typeof(IArticlesRepository), () => new ArticlesRepository(context) },
                { typeof(ICategoriesRepository), () => new CategoriesRepository(context) },
            };
        }
        #endregion

        #region Accessors
        internal Type GetRepositoryType(Type type)
        {
            Type repoType;
            this.modelTypesSpecificTypesMapping.TryGetValue(type, out repoType);
            return repoType;
        }

        internal Func<object> GetGenericFactory<TEntity>() where TEntity : BaseModel
        {
            Func<object> factory;
            this.genericFactories.TryGetValue(typeof(IGenericRepository<TEntity>), out factory);
            return factory;
        }

        internal Func<object> GetSpecificFactory(Type factoryType)
        {
            if (factoryType == null) return null;
            Func<object> factory;
            this.specificFactories.TryGetValue(factoryType, out factory);
            return factory;
        }
        #endregion
    }
}
