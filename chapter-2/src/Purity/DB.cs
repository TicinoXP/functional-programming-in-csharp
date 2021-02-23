using System.Collections.Generic;
using System.Linq;

namespace Purity
{
    internal class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public List<Product> Items { get; set; }

        internal Order WithItems(List<Product> items) =>
            new Order {Id = Id, Status = Status, Items = items};

        public int Conflicts => Items.Count(x => x.Status == "Overwritten");
    }

    internal class Product
    {
        public string Name { get; set; }
        public string Status { get; set; }

        public Product WithStatus(string status) =>
            new Product {Name = Name, Status = status};

        public Product Clone() =>
            new Product {Name = Name, Status = Status};
    }

    internal static class DB
    {
        internal static List<Order> Orders { get; set; } = new List<Order>();
    }

    internal class FunctionalDB
    {
        internal List<Order> Orders { get; set; } = new List<Order>();
    }
}