using Newtonsoft.Json;

namespace Currency_calculator.WebServer
{
    public class JsonRequest
    {
        public JsonRequest()
        {
            this.input = null;
            this.calculatorState = null;
        }

        public JsonRequest(string input, JsonState calculatorState)
        {
            this.input = input;
            this.calculatorState = calculatorState;
        }

        public JsonRequest(string input, string calculatorState)
        {
            this.input = input;
            this.calculatorState = JsonConvert.DeserializeObject<JsonState>(calculatorState);
        }

        public string input { get; set; }

        public JsonState calculatorState { get; set; }
    }
}
