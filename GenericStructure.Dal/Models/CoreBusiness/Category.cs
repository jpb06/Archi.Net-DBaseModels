using GenericStructure.Dal.Models.Base;
using GenericStructure.Dal.Models.CoreBusiness.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericStructure.Dal.Models.CoreBusiness
{
    public class Category : BaseModel, ISalesModel
    {
        /* ----------------------------------------------------------*/

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        /* ----------------------------------------------------------*/
        public virtual ICollection<Article> Articles { get; set; }
        /* ----------------------------------------------------------*/

        public Category() : base()
        {
            this.Articles = new HashSet<Article>();
        }
    }
}
