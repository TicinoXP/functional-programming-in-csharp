using Newtonsoft.Json;

namespace Purity
{
    internal class FunctionalClient
    {
        private readonly FunctionalDB _db;

        public FunctionalClient(FunctionalDB db)
        {
            _db = db;
        }


        internal void PlaceOrder(string jsonName)
        {
            var inputOrder = JsonConvert.DeserializeObject<Order>(System.IO.File.ReadAllText(jsonName));

            var controller = new FunctionalOrderController(_db);
            var order = controller.StartOrder(inputOrder.Id);

            foreach (var item in inputOrder.Items)
                order = controller.AddItemToOrder(order, item);

            var conflicts = order.Conflicts;
            if (conflicts > 10)
                ;
            else if (conflicts > 0)
            {
                WarnUser();
                controller.FinalizeOrder(order);
            }
            else
            {
                controller.FinalizeOrder(order);
            }
        }

        private void WarnUser()
        {
        }
    }
}