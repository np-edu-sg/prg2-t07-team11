//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System.Collections.Generic;

namespace Core.Repository.InMemory
{
    public class Order : IOrder
    {
        private readonly List<Models.Order> _orders = new();

        public Models.Order FindByNo(int no)
        {
            return _orders.Find(o => o.OrderNo == no);
        }

        public List<Models.Order> FindAll()
        {
            return _orders;
        }

        public Models.Order Add(Models.Order order)
        {
            _orders.Add(order);
            return order;
        }

        public void UpdateAmount(int no, double amount)
        {
            var order = FindByNo(no);
            order.Amount = amount;
        }

        public void UpdateStatus(int no, string status)
        {
            var order = FindByNo(no);
            order.Status = status;
        }
    }
}