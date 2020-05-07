using System.Collections.Generic;

namespace Currency_calculator
{
    public class JsonState
    {
        public JsonState()
        {
            IsLastInputAnOperator = false;
            IsLastInputAnEqualsOperator = false;
            Numbers = new List<int>();
            Operators = new List<char>();
            display = string.Empty;
        }

        public string display { get; set; }

        public bool IsLastInputAnOperator { get; set; }

        public bool IsLastInputAnEqualsOperator { get; set; }

        public List<int> Numbers { get; set; }

        public List<char> Operators { get; set; }
    }
}
