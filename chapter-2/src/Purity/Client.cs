using Newtonsoft.Json;

namespace Purity
{
    internal class Client
    {
        internal void PlaceOrder(string jsonName)
        {
            var order = JsonConvert.DeserializeObject<Order>(System.IO.File.ReadAllText(jsonName));

            var controller = new OrderController();
            controller.StartOrder(order.Id);

            var conflicts = 0;
            foreach (var item in order.Items)
            {
                var itemConflicts = 0;
                controller.AddItemToOrder(item, out itemConflicts);
                conflicts += itemConflicts;
            }

            if (conflicts > 10)
                controller.RollBackOrder();
            else if (conflicts > 0)
            {
                WarnUser();
                controller.FinalizeOrder();
            }
            else
            {
                controller.FinalizeOrder();
            }
        }

        private void WarnUser()
        {
        }
    }
}