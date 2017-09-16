using GenericStructure.DataAccessLayer.Manipulation.Repositories;
using GenericStructure.DataAccessLayer.Manipulation.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStructure.DataAccessLayer.Manipulation.Services
{
    public class OrdersService : BaseService
    {
        private OrderRepository orderRepository;
        private OrderDetailRepository orderDetailRepository;

        public OrdersService() : base() { }

        public OrderRepository OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                    this.orderRepository = new OrderRepository(this.context);

                return this.orderRepository;
            }
        }

        public OrderDetailRepository OrderDetailRepository
        {
            get
            {
                if (this.orderDetailRepository == null)
                    this.orderDetailRepository = new OrderDetailRepository(this.context);

                return this.orderDetailRepository;
            }
        }
    }
}
