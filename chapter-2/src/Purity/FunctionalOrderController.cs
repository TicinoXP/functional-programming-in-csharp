using System;
using System.Collections.Generic;
using System.Linq;

namespace Purity
{
    internal class FunctionalOrderController
    {
        private readonly FunctionalDB _db;

        public FunctionalOrderController(FunctionalDB db)
        {
            _db = db;
        }
        
        internal Order GetOrder(int id)
        {
            var q = from o in _db.Orders
                where o.Id == id
                select o;

            return q.SingleOrDefault();
        }

        internal List<Order> GetOrders()
        {
            var q = from o in _db.Orders
                select o;

            return q.ToList();
        }

        internal IEnumerable<Order> GetPendingOrders()
        {
            var q = from o in _db.Orders
                where o.Status == "Pending" 
                select o;

            return q.ToList();
        }

        internal void SetOrderComplete(int id, List<Product> nonCompletedItems)
        {
            var order = GetOrder(id);
            order.Status = "Completed";

            for (var i = 0; i < order.Items.Count; i++)
            {
                var item = order.Items[i];
                if(item.Status != "Completed")
                    nonCompletedItems.Add(item);
                item.Status = "Completed";
            }
        }

        public void FinalizeOrder(Order order)
        {
            if (GetOrder(order.Id) != null)
                throw new Exception("Order already present");
            
            _db.Orders.Add(order);
        }
    }
}