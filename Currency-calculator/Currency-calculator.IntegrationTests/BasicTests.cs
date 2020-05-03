using Currency_calculator.WebServer;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Currency_calculator.IntegrationTests
{
    public class BasicTests
    : IClassFixture<WebApplicationFactory<Currency_calculator.WebServer.Startup>>
    {
        private readonly WebApplicationFactory<Currency_calculator.WebServer.Startup> _factory;

        public BasicTests(WebApplicationFactory<Currency_calculator.WebServer.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_EndpointsReturnSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            JsonRequest jsonRequest = new JsonRequest("1", null);
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            Calculator calculator = new Calculator();
            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_inputIs1CalculatorStateIsNullShouldReturn1()
        {
            // Arrange
            var client = _factory.CreateClient();
            JsonRequest jsonRequest = new JsonRequest("1", null);
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            Calculator calculator = new Calculator();
            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_inputIs1CalculatorStateIs1ShouldReturn11()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);


            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_inputIsPlusCalculatorStateIs1ShouldReturn1()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);
            jsonRequest.input = "+";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);


            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }
    }
}
