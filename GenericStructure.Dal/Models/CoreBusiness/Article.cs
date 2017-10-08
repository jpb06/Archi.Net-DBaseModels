using GenericStructure.Dal.Models.Base;
using GenericStructure.Dal.Models.CoreBusiness.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Models.CoreBusiness
{
    public class Article : BaseModel, ISalesModel
    {
        /* ----------------------------------------------------------*/
        [ForeignKey("Category")]
        public int IdCategory { get; set; }
        /* ----------------------------------------------------------*/

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        [StringLength(2048)]
        public string Description { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public Guid ImagesPath { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        /* ----------------------------------------------------------*/
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        /* ----------------------------------------------------------*/

        public Article() : base()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
