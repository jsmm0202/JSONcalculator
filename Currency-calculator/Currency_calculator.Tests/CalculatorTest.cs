using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace Currency_calculator.Tests
{
    public class CalculatorTest
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

        [Fact]
        public void Test_AllSteps()
        {
            var calculator = new Calculator();
            string jsonState = null;

            //1+2*4-2/5=2

            jsonState = calculator.CalculateNextState(jsonState, "1");
            Assert.Equal("1", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);

            jsonState = calculator.CalculateNextState(jsonState, "+");
            Assert.Equal("1", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);

            jsonState = calculator.CalculateNextState(jsonState, "2");
            Assert.Equal("2", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);

            jsonState = calculator.CalculateNextState(jsonState, "*");
            Assert.Equal("2", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);

            jsonState = calculator.CalculateNextState(jsonState, "4");
            Assert.Equal("4", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);

            jsonState = calculator.CalculateNextState(jsonState, "-");
            Assert.Equal("4", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);

            jsonState = calculator.CalculateNextState(jsonState, "2");
            Assert.Equal("2", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);

            jsonState = calculator.CalculateNextState(jsonState, "/");
            Assert.Equal("2", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);

            jsonState = calculator.CalculateNextState(jsonState, "5");
            Assert.Equal("5", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);

            jsonState = calculator.CalculateNextState(jsonState, "=");
            Assert.Equal("2", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);
        }

        [Fact]
        public void Test_1plus2multiply4equals12()
        {
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "1", "+", "2", "*", "4", "=" };

            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            Assert.Equal("12", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);
        }

        [Fact]
        public void Test_4545mul56minus567equals253953()
        {
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "4", "5", "4", "5", "*", "5", "6", "-", "5", "6", "7", "=" };

            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            Assert.Equal("253953", JsonConvert.DeserializeObject<JsonState>(jsonState).Display);
        }
    }
}
