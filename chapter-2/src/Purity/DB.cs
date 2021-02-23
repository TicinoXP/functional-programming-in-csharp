using System.Collections.Generic;

namespace Purity
{
    internal class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public int Conflicts { get; set; }

        public List<Product> Items { get; set; }
    }

    internal class Product
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }

    internal static class DB
    {
        internal static List<Order> Orders { get; set; } = new List<Order>();
    }
}