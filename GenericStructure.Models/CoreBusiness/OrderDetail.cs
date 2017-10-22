using GenericStructure.Models.Base;
using GenericStructure.Models.CoreBusiness.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericStructure.Models.CoreBusiness
{
    [Table("OrderDetails")]
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
