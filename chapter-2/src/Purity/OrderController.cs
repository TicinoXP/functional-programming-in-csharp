using System;
using System.Collections.Generic;
using System.Linq;

namespace Purity
{
    internal class OrderController
    {
        internal Order GetOrder(int id)
        {
            var q = from o in DB.Orders
                where o.Id == id
                select o;

            return q.SingleOrDefault();
        }

        internal List<Order> GetOrders()
        {
            var q = from o in DB.Orders
                select o;

            return q.ToList();
        }

        internal IEnumerable<Order> GetPendingOrders()
        {
            var q = from o in DB.Orders
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


        private static int _currentOrderId = -1;
        internal void StartOrder(int id)
        {
            if (_currentOrderId != -1)
                throw new Exception("An order is already in progress");
            
            var order = GetOrder(id);
            if (order != null)
                throw new Exception("Order already present");

            order = new Order {Id = id};
            _currentOrderId = order.Id;
            order.Items = new List<Product>();
            
            DB.Orders.Add(order);
        }

        internal void AddItemToOrder(Product product, out int conflicts)
        {
            var order = GetOrder(_currentOrderId);
            if (order == null)
                throw new Exception("No current Order");

            var existingProducts = (from x in order.Items
                where x.Name == product.Name
                select x).ToList();

            conflicts = 0;
            foreach (var item in existingProducts)
            {
                item.Status = "Overwritten";
                conflicts++;
            }

            order.Items.Add(product);
        }

        public void FinalizeOrder()
        {
            var order = GetOrder(_currentOrderId);
            if (order == null)
                throw new Exception("No current Order");

            _currentOrderId = -1;
        }

        public void RollBackOrder()
        {
            var order = GetOrder(_currentOrderId);
            if (order == null)
                throw new Exception("No current Order");
            
            DB.Orders.Remove(order);
            _currentOrderId = -1;
        }
    }
}