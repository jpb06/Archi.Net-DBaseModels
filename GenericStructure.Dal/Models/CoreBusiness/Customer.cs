using GenericStructure.Dal.Models.Base;
using GenericStructure.Dal.Models.CoreBusiness.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
