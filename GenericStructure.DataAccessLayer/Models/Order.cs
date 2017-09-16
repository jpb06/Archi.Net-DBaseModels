using GenericStructure.DataAccessLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.DataAccessLayer.Models
{
    public class Order : BaseModel
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
