using GenericStructure.Models.Base;
using GenericStructure.Models.CoreBusiness.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericStructure.Models.CoreBusiness
{
    [Table("Orders")]
    public class Order : BaseModel, IOrdersModel
    {
        /* ----------------------------------------------------------*/
        [ForeignKey("Customer")]
        public int IdCustomer { get; set; }
        /* ----------------------------------------------------------*/

        [Required]
        [StringLength(256)]
        public string Reference { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime ShipDate { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentCardMember { get; set; }

        [Required]
        public DateTime PaymentCardExpiration { get; set; }

        /* ----------------------------------------------------------*/
        public Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        /* ----------------------------------------------------------*/

        public Order() : base()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
