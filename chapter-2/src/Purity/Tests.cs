using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Purity
{
    public class Tests
    {
        [Fact]
        void should_place_order()
        {
            var client = new Client();
            client.PlaceOrder("order.json");

            var order = DB.Orders.Single();
            order.Items.Count.Should().Be(3);
            order.Items.Select(x => x.Status).Should().BeEquivalentTo(
                new List<string> {null, "Overwritten", null});
        }

        [Fact]
        void should_rollbace()
        {
            var client = new Client();
            client.PlaceOrder("badOrder.json");

            var order = DB.Orders.Count.Should().Be(0);
        }
    }
}