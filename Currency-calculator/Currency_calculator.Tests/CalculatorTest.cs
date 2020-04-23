using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace Currency_calculator.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test_1plus2equals3()
        {
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "1", "+", "2", "=" };

            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            Assert.Equal("3", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);
        }

        [Fact]
        public void Test_2multiply3equals6()
        {
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "2", "*", "3", "=" };

            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            Assert.Equal("6", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);
        }

        [Fact]
        public void Test_81minus11equals70()
        {
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "8", "1", "-", "1", "1", "=" };

            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            Assert.Equal("70", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);
        }

        [Fact]
        public void Test_45div5equals9()
        {
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "4", "5", "/", "5", "=" };

            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            Assert.Equal("9", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);
        }
    }
}
