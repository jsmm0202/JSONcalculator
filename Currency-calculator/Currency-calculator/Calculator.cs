using System;
using Newtonsoft.Json;

namespace Currency_calculator
{
    public class Calculator
    {
        // the expected length of each input
        private const int ONE_CHAR_STRING_LENGTH = 1;

        private JsonState jsonStateObj;

        public string CalculateNextState(string jsonState, string input)
        {
            // if it is the first time the function is called, creat a new object
            if (jsonState == null)
            {
                jsonStateObj = new JsonState();
            }
            else
            {
                // if it is not the first time, deserialize the json object
                jsonStateObj = JsonConvert.DeserializeObject<JsonState>(jsonState);
            }

            HandleInput(input);

            return JsonConvert.SerializeObject(jsonStateObj);
        }

        private void HandleInput(string input)
        {
            // if input is not only one character throw an exeption
            if (input.Length != ONE_CHAR_STRING_LENGTH)
            {
                throw new ArgumentException(
                    @$"{input} is an invalid input.
Input must be only one character");
            }

            switch (input[0])
            {
                // the input is a digit
                case char inp when inp >= '0' && inp <= '9':
                    InputIsADigit(inp);
                    break;

                // the input is a legal operator
                case char inp when inp == '+' ||
                                   inp == '-' ||
                                   inp == '*' ||
                                   inp == '/':
                    InputIsAnOperator(inp);
                    break;

                // the digit is the "=" operator
                case char inp when inp == '=':
                    InputIsAnEqualsOp();
                    break;

                // if the input is none of the above, it is invalid.
                default:
                    throw new ArgumentException(
                    @$"{input} is an invalid input.
Input must be a digit between 0-9 or one of the operators +-*/=");
            }
        }

        private void InputIsADigit(char inp)
        {
            // if the last input was an operator, we need to reset the display value
            if (jsonStateObj.IsLastInputAnOperator)
            {
                jsonStateObj.Display = string.Empty;
            }

            // add the new input to the existing value on the display
            jsonStateObj.Display += inp;

            jsonStateObj.IsLastInputAnOperator = false;
        }

        private void InputIsAnOperator(char inp)
        {
            // if the last input was an operator then this is an invalid input,
            // because two operators must be separated by numbers
            if (jsonStateObj.IsLastInputAnOperator)
            {
                throw new ArgumentException(
                    @"Invalid input.
Two operators must be separated by numbers.");
            }

            // add the operator to the operators list
            jsonStateObj.Operators.Add(inp);

            // add the last dispayed number to the list of numbers
            jsonStateObj.Numbers.Add(int.Parse(jsonStateObj.Display));

            jsonStateObj.IsLastInputAnOperator = true;
        }

        private void InputIsAnEqualsOp()
        {
            // if the last input was an operator then this is an invalid input,
            // because two operators must be separated by numbers
            if (jsonStateObj.IsLastInputAnOperator)
            {
                throw new ArgumentException(
                    @"Invalid input.
Two operators must be separated by numbers.");
            }

            // add the last dispayed number to the list of numbers
            jsonStateObj.Numbers.Add(int.Parse(jsonStateObj.Display));

            // in a valid equation the number of numerals is grater by one than the number of operators
            if (jsonStateObj.Numbers.Count - 1 != jsonStateObj.Operators.Count)
            {
                throw new ArgumentException(
                    @"Invalid input.
The Structure of the equation is not right.");
            }

            // iterate over the lists of numbers and operators by the order of the input
            // and comute the final value that needs to be displayed
            int res = jsonStateObj.Numbers[0];
            for (int i = 1; i < jsonStateObj.Numbers.Count; i++)
            {
                switch (jsonStateObj.Operators[i - 1])
                {
                    case '+':
                        res += jsonStateObj.Numbers[i];
                        break;
                    case '-':
                        res -= jsonStateObj.Numbers[i];
                        break;
                    case '*':
                        res *= jsonStateObj.Numbers[i];
                        break;
                    case '/':
                        res /= jsonStateObj.Numbers[i];
                        break;
                }
            }

            jsonStateObj.Display = res.ToString();
        }
    }
}
