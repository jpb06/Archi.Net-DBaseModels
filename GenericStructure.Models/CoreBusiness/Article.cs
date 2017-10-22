using GenericStructure.Models.Base;
using GenericStructure.Models.CoreBusiness.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericStructure.Models.CoreBusiness
{
    [Table("Articles")]
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
