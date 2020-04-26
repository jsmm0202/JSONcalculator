using System;
using Xunit;

namespace Currency_calculator.Tests
{
    public class ExeptionsTest
    {
        [Fact]
        public void Test_InputMustBeOnlyOneCharacter()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;

            //act
            try
            {
                jsonState = calculator.CalculateNextState(jsonState, "11");
            }
            catch (Exception ex)
            {
                //assert
                Assert.Equal("11 is an invalid input.\nInput must be only one character", ex.Message);
            }
        }

        [Fact]
        public void Test_InputMustBeDigitOrOperator()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;

            //act
            try
            {
                jsonState = calculator.CalculateNextState(jsonState, "a");
            }
            catch (Exception ex)
            {
                //assert
                Assert.Equal("a is an invalid input.\nInput must be a digit between 0-9 or one of the operators +-*/=", ex.Message);
            }
        }

        [Fact]
        public void Test_TwoOperatorsMustBeSeparatedByNumbers()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;
            jsonState = calculator.CalculateNextState(jsonState, "1");
            jsonState = calculator.CalculateNextState(jsonState, "+");

            //act
            try
            {
                jsonState = calculator.CalculateNextState(jsonState, "-");
            }
            catch (Exception ex)
            {
                //assert
                Assert.Equal("Invalid input.\nTwo operators must be separated by numbers.", ex.Message);
            }
        }

        [Fact]
        public void Test_OperatorAndEqualsOperatorMustBeSeparatedByNumbers()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;
            jsonState = calculator.CalculateNextState(jsonState, "1");
            jsonState = calculator.CalculateNextState(jsonState, "+");

            //act
            try
            {
                jsonState = calculator.CalculateNextState(jsonState, "=");
            }
            catch (Exception ex)
            {
                //assert
                Assert.Equal("Invalid input.\nTwo operators must be separated by numbers.", ex.Message);
            }
        }

        [Fact]
        public void Test_AttemptedToDivideByZero()
        {
            //arrange
            var calculator = new Calculator();
            string jsonState = null;
            jsonState = calculator.CalculateNextState(jsonState, "5");
            jsonState = calculator.CalculateNextState(jsonState, "/");
            jsonState = calculator.CalculateNextState(jsonState, "0");

            //act
            try
            {
                jsonState = calculator.CalculateNextState(jsonState, "=");
            }
            catch (Exception ex)
            {
                //assert
                Assert.Equal(@"Attempted to divide by zero.", ex.Message);
            }
        }
    }
}
