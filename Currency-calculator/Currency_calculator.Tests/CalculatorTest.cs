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
            //arrange
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "1", "+", "2", "=" };

            //act
            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            //assert
            Assert.Equal("3", JsonConvert.DeserializeObject<JsonState>(jsonState).display);
        }

        [Fact]
        public void Test_2multiply3equals6()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "2", "*", "3", "=" };

            //act
            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            //assert
            Assert.Equal("6", JsonConvert.DeserializeObject<JsonState>(jsonState).display);
        }

        [Fact]
        public void Test_81minus11equals70()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "8", "1", "-", "1", "1", "=" };

            //act
            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            //assert
            Assert.Equal("70", JsonConvert.DeserializeObject<JsonState>(jsonState).display);
        }

        [Fact]
        public void Test_45div5equals9()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "4", "5", "/", "5", "=" };

            //act
            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            //assert
            Assert.Equal("9", JsonConvert.DeserializeObject<JsonState>(jsonState).display);
        }

        [Fact]
        public void Test_RoundDown7div2equals3()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "7", "/", "2", "=" };

            //act
            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            //assert
            Assert.Equal("3", JsonConvert.DeserializeObject<JsonState>(jsonState).display);
        }

        [Fact]
        public void Test_AllSteps()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;

            //the calculation performed is: 1+2*4-2/5=2

            //act and assert
            jsonState = calculator.CalculateNextState(jsonState, "1");
            Assert.Equal("1", JsonConvert.DeserializeObject<JsonState>(jsonState).display);

            jsonState = calculator.CalculateNextState(jsonState, "+");
            Assert.Equal("1", JsonConvert.DeserializeObject<JsonState>(jsonState).display);

            jsonState = calculator.CalculateNextState(jsonState, "2");
            Assert.Equal("2", JsonConvert.DeserializeObject<JsonState>(jsonState).display);

            jsonState = calculator.CalculateNextState(jsonState, "*");
            Assert.Equal("2", JsonConvert.DeserializeObject<JsonState>(jsonState).display);

            jsonState = calculator.CalculateNextState(jsonState, "4");
            Assert.Equal("4", JsonConvert.DeserializeObject<JsonState>(jsonState).display);

            jsonState = calculator.CalculateNextState(jsonState, "-");
            Assert.Equal("4", JsonConvert.DeserializeObject<JsonState>(jsonState).display);

            jsonState = calculator.CalculateNextState(jsonState, "2");
            Assert.Equal("2", JsonConvert.DeserializeObject<JsonState>(jsonState).display);

            jsonState = calculator.CalculateNextState(jsonState, "/");
            Assert.Equal("2", JsonConvert.DeserializeObject<JsonState>(jsonState).display);

            jsonState = calculator.CalculateNextState(jsonState, "5");
            Assert.Equal("5", JsonConvert.DeserializeObject<JsonState>(jsonState).display);

            jsonState = calculator.CalculateNextState(jsonState, "=");
            Assert.Equal("2", JsonConvert.DeserializeObject<JsonState>(jsonState).display);
        }

        [Fact]
        public void Test_1plus2multiply4equals12()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "1", "+", "2", "*", "4", "=" };

            //act
            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            //assert
            Assert.Equal("12", JsonConvert.DeserializeObject<JsonState>(jsonState).display);
        }

        [Fact]
        public void Test_4545mul56minus567equals253953()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;

            var inputs = new List<string>() { "4", "5", "4", "5", "*", "5", "6", "-", "5", "6", "7", "=" };

            //act
            foreach (var input in inputs)
            {
                jsonState = calculator.CalculateNextState(jsonState, input);
            }

            //assert
            Assert.Equal("253953", JsonConvert.DeserializeObject<JsonState>(jsonState).display);
        }
    }
}
