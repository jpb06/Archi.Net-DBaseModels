using GenericStructure.Models.Base;
using GenericStructure.Models.CoreBusiness.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericStructure.Models.CoreBusiness
{
    [Table("Categories")]
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
