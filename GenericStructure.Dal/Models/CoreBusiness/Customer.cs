using GenericStructure.Dal.Models.Base;
using GenericStructure.Dal.Models.CoreBusiness.Contracts;
using System.ComponentModel.DataAnnotations;

namespace GenericStructure.Dal.Models.CoreBusiness
{
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
