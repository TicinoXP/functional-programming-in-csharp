using System.Linq;
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

            var order = inputOrder.Items.Aggregate(
                inputOrder.Id.StartOrder(),
                (current, item) => current.AddItem(item));

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