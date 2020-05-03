namespace Currency_calculator.WebServer
{
    public class JsonRequest
    {
        public JsonRequest(string input, string calculatorState)
        {
            this.input = input;
            this.calculatorState = calculatorState;
        }

        public string input { get; set; }

        public string calculatorState { get; set; }
    }
}
