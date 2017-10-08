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
    public class OrderDetail : BaseModel, IOrdersModel
    {
        /* ----------------------------------------------------------*/
        [ForeignKey("Order")]
        public int IdOrder { get; set; }

        [ForeignKey("Article")]
        public int IdArticle { get; set; }
        /* ----------------------------------------------------------*/

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitCost { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? LineItemTotal { get; set; }

        /* ----------------------------------------------------------*/
        public virtual Order Order { get; set; }
        public virtual Article Article { get; set; }
        /* ----------------------------------------------------------*/

        public OrderDetail() : base()
        {

        }
    }
}
