using GenericStructure.Dal.Context.Contracts;
using GenericStructure.Models.CoreBusiness;
using System.Data.Entity;

namespace GenericStructure.Dal.Context.Base
{
    public class CoreBusinessBaseContext : DbContext, ICoreBusinessContext
    {
        public CoreBusinessBaseContext(string connectionString)
            : base(connectionString)
        {
        }

        public virtual IDbSet<Customer> Customers { get; set; }
        public virtual IDbSet<Article> Articles { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<Order> Orders { get; set; }
        public virtual IDbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var articleModel = modelBuilder.Entity<Article>();
            articleModel.HasKey(t => t.Id);
            articleModel.Property(t => t.RowVersion).IsFixedLength();
            //articleModel.Property(t => t.Price).HasPrecision(19, 4);

            var categoryModel = modelBuilder.Entity<Category>();
            categoryModel.HasKey(t => t.Id);
            categoryModel.Property(t => t.RowVersion).IsFixedLength();

            var orderModel = modelBuilder.Entity<Order>();
            orderModel.HasKey(t => t.Id);
            orderModel.Property(t => t.RowVersion).IsFixedLength();

            var customersModel = modelBuilder.Entity<Customer>();
            customersModel.HasKey(t => t.Id);
            customersModel.Property(t => t.RowVersion).IsFixedLength();

            var orderDetailsModel = modelBuilder.Entity<OrderDetail>();
            orderDetailsModel.HasKey(t => t.Id);
            orderDetailsModel.Property(t => t.RowVersion).IsFixedLength();
        }
    }
}
