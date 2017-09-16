using GenericStructure.DataAccessLayer.Manipulation.Repositories;
using GenericStructure.DataAccessLayer.Manipulation.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.DataAccessLayer.Manipulation.Services
{
    public class SalesService : BaseService
    {
        private ArticleRepository articleRepository;
        private CategoryRepository categoryRepository;

        public SalesService() : base() { }

        public ArticleRepository ArticleRepository
        {
            get
            {
                if (this.articleRepository == null)
                    this.articleRepository = new ArticleRepository(this.context);

                return this.articleRepository;
            }
        }

        public CategoryRepository CategoriesRepository
        {
            get
            {
                if (this.categoryRepository == null)
                    this.categoryRepository = new CategoryRepository(this.context);

                return this.categoryRepository;
            }
        }
    }
}
