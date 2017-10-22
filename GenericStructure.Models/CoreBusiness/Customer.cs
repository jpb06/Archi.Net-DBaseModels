using GenericStructure.Models.Base;
using GenericStructure.Models.CoreBusiness.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericStructure.Models.CoreBusiness
{
    [Table("Customers")]
    public class Customer : BaseModel, ICustomerModel
    {
        /* ----------------------------------------------------------*/

        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(128)]
        public string LastName { get; set; }

        [Required]
        [StringLength(128)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        /* ----------------------------------------------------------*/

        public Customer() : base()
        {

        }

    }
}
