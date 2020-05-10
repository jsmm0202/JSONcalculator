using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Currency_calculator.WebServer;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Currency_calculator.IntegrationTests
{
    public class InputResponseTests
    : IClassFixture<WebApplicationFactory<Currency_calculator.WebServer.Startup>>
    {
        private readonly WebApplicationFactory<Currency_calculator.WebServer.Startup> _factory;

        public InputResponseTests(WebApplicationFactory<Currency_calculator.WebServer.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_EndpointsReturnSuccess()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState) null);

            Calculator calculator = new Calculator();

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
        public async Task Test_inputIs1_CalculatorStateIsNull_ShouldReturn1()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();

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
        public async Task Test_inputIs2_CalculatorStateIs1_ShouldReturn12()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 (display: 1)
            jsonRequest.input = "2";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);
            // calculator state is 12 (display: 12)

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_inputIsPlus_CalculatorStateIs1_ShouldReturn1()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 (display: 1)
            jsonRequest.input = "+";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);
            // calculator state is 1 + (display: 1)

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_inputIs2_CalculatorStateIs1Plus_ShouldReturn2()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 (display: 1)
            jsonRequest.input = "+";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + (display: 1)
            jsonRequest.input = "2";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);
            // calculator state is 1 + 2 (display: 2)

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_inputIsMultiply_CalculatorStateIs1Plus2_ShouldReturn2()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 (display: 1)
            jsonRequest.input = "+";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + (display: 1)
            jsonRequest.input = "2";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + 2 (display: 2)
            jsonRequest.input = "*";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);
            // calculator state is 1 + 2 * (display: 2)

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_inputIs4_CalculatorStateIs1Plus2Multiply_ShouldReturn4()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 (display: 1)
            jsonRequest.input = "+";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + (display: 1)
            jsonRequest.input = "2";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + 2 (display: 2)
            jsonRequest.input = "*";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + 2 * (display: 2)
            jsonRequest.input = "4";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);
            // calculator state is 1 + 2 * 4 (display: 4)

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_inputIsEquals_CalculatorStateIs1Plus2Multiply4_ShouldReturn12()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 (display: 1)
            jsonRequest.input = "+";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + (display: 1)
            jsonRequest.input = "2";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + 2 (display: 2)
            jsonRequest.input = "*";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + 2 * (display: 2)
            jsonRequest.input = "4";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + 2 * 4 (display: 4)
            jsonRequest.input = "=";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);
            // calculator state is 1 + 2 * 4 = (display: 12)

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_newCalculationAfterEqualsSign_ShouldReturn1()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 (display: 1)
            jsonRequest.input = "+";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + (display: 1)
            jsonRequest.input = "2";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + 2 (display: 2)
            jsonRequest.input = "=";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + 2 = (display: 3)
            jsonRequest.input = "1";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input);
            // new calculation after equals sign - calculator state is 1 (display: 1)

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(expectedResponse, strResponse);
        }
    }
}
