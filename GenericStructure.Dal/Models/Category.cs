using GenericStructure.Dal.Models.Base;
using GenericStructure.Dal.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Models
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
