using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Purity
{
    public class Tests
    {
        [Fact]
        void should_pass()
        {
            var client = new Client();
            client.PlaceOrder();

            var order = DB.Orders.Single();
            order.Items.Count.Should().Be(3);
            order.Items.Select(x => x.Status).Should().BeEquivalentTo(
                new List<string> {null, "Overwritten", null});
        }
    }
}