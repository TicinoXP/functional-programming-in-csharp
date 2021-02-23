using System.Collections.Generic;
using System.Linq;

namespace Purity
{
    internal static class OrderExtensions
    {
        internal static Order StartOrder(this int id) => 
            new Order {Id = id, Items = new List<Product>()};

        internal static Order AddItem(this Order order, Product product) =>
            order.WithItems(
                order.Items.Select(item => 
                    item.Name == product.Name
                        ? item.WithStatus("Overwritten")
                        : item.Clone())
                .Union(new List<Product> {product})
                .ToList());
    }
}