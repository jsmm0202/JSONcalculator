using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Currency_calculator
{
    public class Calculator
    {
        public string CalculateNextState(JsonState jsonState, string input)
        {
            string calculatorState = (jsonState != null) ? JsonConvert.SerializeObject(jsonState) : null;
            return CalculateNextState(calculatorState, input);
        }

        public string CalculateNextState(string jsonState, string input)
        {
            JsonState jsonStateObj;

            // if it is the first time the function is called, create a new object
            if (jsonState == null)
            {
                jsonStateObj = new JsonState();
            }
            else
            {
                // if it is not the first time, deserialize the json object
                jsonStateObj = JsonConvert.DeserializeObject<JsonState>(jsonState);

                // if it is called after previous calculation is done, create a new object
                if (jsonStateObj.IsLastInputAnEqualsOperator || jsonStateObj.IsLastInputInvalid)
                {
                    jsonStateObj = new JsonState();
                }
            }

            ValidateInput(input, jsonStateObj);

            HandleInput(input, jsonStateObj);

            return JsonConvert.SerializeObject(jsonStateObj);
        }

        private void ValidateInput(string input, JsonState jsonStateObj)
        {
            // if input is not only one character throw an exeption
            if (input.Length != 1)
            {
                throw new ArgumentException(
                    $"{input} is an invalid input.\nInput must be only one character");
            }

            // if the input is not a digit or one of the operators +-*/=, it is invalid.
            if (!Regex.IsMatch(input, @"[0-9+\-*/=]"))
            {
                throw new ArgumentException(
                $"{input} is an invalid input.\nInput must be a digit between 0-9 or one of the operators +-*/=");
            }

            // when input is an operator
            if (Regex.IsMatch(input, @"[+\-*/=]"))
            {
                // if the last input was an operator then this is an invalid input,
                // because two operators must be separated by numbers
                if (jsonStateObj.IsLastInputAnOperator)
                {
                    throw new ArgumentException(
                        "Invalid input.\nTwo operators must be separated by numbers.");
                }
            }
        }

        private void HandleInput(string input, JsonState jsonStateObj)
        {
            switch (input[0])
            {
                // the input is a digit
                case char inp when char.IsDigit(inp):
                    HandleDigitInput(inp, jsonStateObj);
                    break;

                // the input is a legal operator
                case char inp when Regex.IsMatch(inp.ToString(), @"[+\-*/]"):
                    HandleOperatorInput(inp, jsonStateObj);
                    break;

                case char inp when inp == '=':
                    HandleEqualsOperator(jsonStateObj);
                    break;
            }
        }

        private void HandleDigitInput(char inp, JsonState jsonStateObj)
        {
            // if the last input was an operator, we need to reset the display value
            if (jsonStateObj.IsLastInputAnOperator)
            {
                jsonStateObj.display = string.Empty;
            }

            // add the new input to the existing value on the display
            jsonStateObj.display += inp;

            jsonStateObj.IsLastInputAnOperator = false;
        }

        private void HandleOperatorInput(char inp, JsonState jsonStateObj)
        {
            // add the operator to the operators list
            jsonStateObj.Operators.Add(inp);

            // add the last displayed number to the list of numbers
            jsonStateObj.Numbers.Add(int.Parse(jsonStateObj.display));

            jsonStateObj.IsLastInputAnOperator = true;
        }

        private void HandleEqualsOperator(JsonState jsonStateObj)
        {
            // add the last dispayed number to the list of numbers
            jsonStateObj.Numbers.Add(int.Parse(jsonStateObj.display));

            // in a valid equation the number of numerals is grater by one than the number of operators
            if (jsonStateObj.Numbers.Count - 1 != jsonStateObj.Operators.Count)
            {
                throw new ArgumentException(
                    "Invalid input.\nThe Structure of the equation is not right.");
            }

            // iterate over the lists of numbers and operators by the order of the input
            // and compute the final value that needs to be displayed
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

            jsonStateObj.display = res.ToString();

            jsonStateObj.IsLastInputAnEqualsOperator = true;
        }
    }
}
