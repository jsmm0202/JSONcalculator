﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Currency_calculator.WebServer;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Currency_calculator.IntegrationTests
{
    public class InvalidInputTests
    : IClassFixture<WebApplicationFactory<Currency_calculator.WebServer.Startup>>
    {
        private readonly WebApplicationFactory<Currency_calculator.WebServer.Startup> _factory;

        public InvalidInputTests(WebApplicationFactory<Currency_calculator.WebServer.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_InputMustBeOnlyOneCharacter()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("11", (JsonState)null);

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = CreateErrorJsonState();

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            // Exception message : "11 is an invalid input.\nInput must be only one character"
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_InputMustBeDigitOrOperator()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("a", (JsonState)null);

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = CreateErrorJsonState();

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            // Exception message : "a is an invalid input.\nInput must be a digit between 0-9 or one of the operators +-*/="
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_TwoOperatorsMustBeSeparatedByNumbers()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 (display: 1)
            jsonRequest.input = "+";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 + (display: 1)
            jsonRequest.input = "-";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = CreateErrorJsonState();

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            // Exception message : "Invalid input.\nTwo operators must be separated by numbers."
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_OperatorAndEqualsOperatorMustBeSeparatedByNumbers()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 (display: 1)
            jsonRequest.input = "+";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));

            // calculator state is 1 + (display: 1)
            jsonRequest.input = "=";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = CreateErrorJsonState();

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            // Exception message : "Invalid input.\nTwo operators must be separated by numbers."
            Assert.Equal(expectedResponse, strResponse);
        }

        [Fact]
        public async Task Test_AttemptedToDivideByZero()
        {
            // Arrange
            JsonRequest jsonRequest = new JsonRequest("1", (JsonState)null);

            Calculator calculator = new Calculator();
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 (display: 1)
            jsonRequest.input = "/";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 / (display: 1)
            jsonRequest.input = "0";
            jsonRequest.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculator.CalculateNextState(jsonRequest.calculatorState, jsonRequest.input));
            // calculator state is 1 / 0 (display: 0)
            jsonRequest.input = "=";

            var client = _factory.CreateClient();
            var myContent = JsonConvert.SerializeObject(jsonRequest);
            Uri uri = new Uri("http://localhost:3000/calculate");
            HttpContent content = new StringContent(myContent, Encoding.UTF8, "application/json");

            string expectedResponse = CreateErrorJsonState();

            //// Act
            var response = await client.PostAsync(uri, content);
            string strResponse = response.Content.ReadAsStringAsync().Result;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            // Exception message : "Attempted to divide by zero."
            Assert.Equal(expectedResponse, strResponse);
        }

        private string CreateErrorJsonState()
        {
            JsonState errorJsonState = new JsonState
            {
                IsLastInputInvalid = true,
                display = "Invalid operation, reset",
            };
            return JsonConvert.SerializeObject(errorJsonState);
        }
    }
}
