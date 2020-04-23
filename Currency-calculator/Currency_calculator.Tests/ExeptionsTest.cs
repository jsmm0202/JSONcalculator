using System;
using Xunit;

namespace Currency_calculator.Tests
{
    public class ExeptionsTest
    {
        [Fact]
        public void Test_InputMustBeOnlyOneCharacter()
        {
            var calculator = new Calculator();
            string jsonState = null;

            try
            {
                jsonState = calculator.CalculateNextState(jsonState, "11");
            }
            catch (Exception ex)
            {
                Assert.Equal(@"11 is an invalid input.
Input must be only one character", ex.Message);
            }
        }

        [Fact]
        public void Test_InputMustBeDigitOrOperator()
        {
            var calculator = new Calculator();
            string jsonState = null;

            try
            {
                jsonState = calculator.CalculateNextState(jsonState, "a");
            }
            catch (Exception ex)
            {
                Assert.Equal(@"a is an invalid input.
Input must be a digit between 0-9 or one of the operators +-*/=", ex.Message);
            }
        }

        [Fact]
        public void Test_TwoOperatorsMustBeSeparatedByNumbers()
        {
            var calculator = new Calculator();
            string jsonState = null;
            jsonState = calculator.CalculateNextState(jsonState, "1");
            jsonState = calculator.CalculateNextState(jsonState, "+");
            try
            {
                jsonState = calculator.CalculateNextState(jsonState, "-");
            }
            catch (Exception ex)
            {
                Assert.Equal(@"Invalid input.
Two operators must be separated by numbers.", ex.Message);
            }
        }

        [Fact]
        public void Test_OperatorAndEqualsOperatorMustBeSeparatedByNumbers()
        {
            var calculator = new Calculator();
            string jsonState = null;
            jsonState = calculator.CalculateNextState(jsonState, "1");
            jsonState = calculator.CalculateNextState(jsonState, "+");
            try
            {
                jsonState = calculator.CalculateNextState(jsonState, "=");
            }
            catch (Exception ex)
            {
                Assert.Equal(@"Invalid input.
Two operators must be separated by numbers.", ex.Message);
            }
        }
    }
}
