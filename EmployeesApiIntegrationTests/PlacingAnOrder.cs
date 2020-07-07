using EmployeesApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeesApiIntegrationTests
{
    public class PlacingAnOrder : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public PlacingAnOrder(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PlacingAnOrderBeforeCutoffShipsToday()
        {
            //GIVEN
            //it is befoer the cutoff
            _factory.SystemTimeToUse = new DateTime(1992, 4, 15, 11, 59, 59);
            //WHEN
            //I place an order
            var response = await _client.PostAsJsonAsync("/orders", new OrderToSend());
            //THEN
            //it ships the same day
            var content = await response.Content.ReadAsAsync<OrderConfirmation>();
        }

        [Fact]
        public async Task PlacingAnOrderAfterCutoffShipsTomorrow()
        {
            //GIVEN
            //it is after the cutoff
            _factory.SystemTimeToUse = new DateTime(1992, 4, 15, 12, 00, 00);
            //WHEN
            //I place an order
            var response = await _client.PostAsJsonAsync("/orders", new OrderToSend());
            //THEN
            //it ships the next day.
            var content = await response.Content.ReadAsAsync<OrderConfirmation>();
        }
    }

    public class OrderToSend
    {

    }

    public class OrderConfirmation
    {
     public DateTime estimatedShipDate { get; set; }
    }

    }
