using GenericStructure.Models.CoreBusiness;
using System.Data.Entity;

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
