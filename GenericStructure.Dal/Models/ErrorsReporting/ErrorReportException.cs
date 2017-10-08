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
    [Table("Exceptions")]
    public class ErrorReportException : BaseModel
    {
        /* ----------------------------------------------------------*/
        [ForeignKey("InnerException")]
        public int? IdInnerException { get; set; }

        [ForeignKey("Application")]
        public int IdApplication { get; set; }
        /* ----------------------------------------------------------*/

        [Required]
        [StringLength(512)]
        public string Type { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        [StringLength(512)]
        public string Source { get; set; }

        [Required]
        [StringLength(512)]
        public string SiteModule{ get; set; }

        [Required]
        [StringLength(512)]
        public string SiteName { get; set; }

        [Required]
        public string StackTrace { get; set; }

        [StringLength(512)]
        public string CustomErrorType { get; set; }

        [StringLength(512)]
        public string HelpLink { get; set; }

        [Required]
        public DateTime Date { get; set; }

        /* ----------------------------------------------------------*/
        public virtual ErrorReportApplication Application { get; set; }

        public virtual ErrorReportException InnerException { get; set; }
        /* ----------------------------------------------------------*/

        public ErrorReportException()
            : base()
        {

        }
    }
}
