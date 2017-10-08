using GenericStructure.Dal.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Models.ErrorsReporting
{
    [Table("Applications")]
    public class ErrorReportApplication : BaseModel
    {
        /* ----------------------------------------------------------*/

        [Required]
        [Index("IX_NameAndVersion", 1, IsUnique = true)]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [Index("IX_NameAndVersion", 2, IsUnique = true)]
        [StringLength(128)]
        public string Version { get; set; }

        public DateTime FirstRunDate { get; set; }

        /* ----------------------------------------------------------*/
        public virtual ICollection<ErrorReportException> Exceptions { get; set; }
        /* ----------------------------------------------------------*/

        public ErrorReportApplication()
            : base()
        {
            this.Exceptions = new HashSet<ErrorReportException>();
        }
    }
}
