using GenericStructure.Dal.Models.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.Dal.Context.Contracts
{
    public interface ICoreBusinessContext : IDbContext
    {
        IDbSet<Customer> Customers { get; set; }
        IDbSet<Article> Articles { get; set; }
        IDbSet<Category> Categories { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<OrderDetail> OrderDetails { get; set; }
    }
}
