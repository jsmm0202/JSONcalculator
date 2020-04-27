using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Currency_calculator.WebServer
{
    internal class Program
    {
        public static string SendResponse(HttpListenerRequest request)
        {
            string stringRequest = string.Empty;
            using (Stream body = request.InputStream)
            {
                if (body != null)
                {
                    using (StreamReader reader = new StreamReader(body))
                    {
                        stringRequest = reader.ReadToEnd();
                    }
                }
            }

            string input = JsonConvert.DeserializeObject<JsonRequest>(stringRequest).input;
            string calculatorState = JsonConvert.DeserializeObject<JsonRequest>(stringRequest).calculatorState;

            var calculator = new Calculator();
            string jsonState;
            try
            {
                jsonState = calculator.CalculateNextState(calculatorState, input);
            }
            catch (Exception ex)
            {
                jsonState = ex.Message;
            }

            Console.WriteLine(jsonState);
            return jsonState;
        }

        private static void Main(string[] args)
        {
            try
            {
                var ws = new WebServer(SendResponse, "http://localhost:3000/calculate/");
                ws.Run();
                Console.WriteLine("Press a key to quit.");
                Console.ReadKey();
                ws.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}