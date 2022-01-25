using System.Collections.Generic;

namespace Core.Repository.InMemory
{
    public class Order : IOrder
    {
        private readonly List<Models.Order> _orders = new();

        public Models.Order FindByNo(int no) => _orders.Find(o => o.OrderNo == no);

        public List<Models.Order> FindAll() => _orders;

        public Models.Order Add(Models.Order order)
        {
            _orders.Add(order);
            return order;
        }
    }
}