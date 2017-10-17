using GenericStructure.Dal.Models.ErrorsReporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Tests.Data.Mocked
{
    public class VolatileErrorsReportingDataset
    {
        public List<ErrorReportApplication> Applications { get; set; }
        public List<ErrorReportException> Exceptions { get; set; }

        public VolatileErrorsReportingDataset()
        {
            this.Applications = new List<ErrorReportApplication>();
            this.Exceptions = new List<ErrorReportException>();

            this.Populate();
        }

        public void Populate()
        {
            #region Applications
            this.Applications.AddRange(new List<ErrorReportApplication>()
            { 
                new ErrorReportApplication
                {
                    Id = 1,
                    Name = "TestApplicationAlreadyExisting",
                    Version = "1.0.0.0",
                    FirstRunDate = new DateTime(2000, 1, 1), 
                    RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }
                },
                new ErrorReportApplication
                {
                    Id = 2,
                    Name = "TestApplicationForVersion",
                    Version = "1.0.0.1",
                    FirstRunDate = new DateTime(2100, 1, 1),
                    RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }
                },
                new ErrorReportApplication
                {
                    Id = 3,
                    Name = "TestApplicationForUpdate",
                    Version = "1.0.0.0",
                    FirstRunDate = new DateTime(2200, 1, 1),
                    RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }
                },
                new ErrorReportApplication
                {
                    Id = 4,
                    Name = "TestApplicationForDelete",
                    Version = "1.0.0.0",
                    FirstRunDate = new DateTime(2300, 1, 1),
                    RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 1 }
                },
            });
            #endregion
        }
    }
}
