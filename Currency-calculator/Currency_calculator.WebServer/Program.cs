using System;
using System.Net;
using Newtonsoft.Json;

namespace Currency_calculator.WebServer
{
    internal class Program
    {
        public static string SendResponse(HttpListenerRequest request)
        {
            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);

            string stringRequest = reader.ReadToEnd();
            body.Close();
            reader.Close();

            string input = JsonConvert.DeserializeObject<JsonRequest>(stringRequest).input;
            string calculatorState = JsonConvert.DeserializeObject<JsonRequest>(stringRequest).calculatorState;

            var calculator = new Calculator();
            var jsonState = calculator.CalculateNextState(calculatorState, input);
            Console.WriteLine(jsonState);
            return jsonState;
        }

        private static void Main(string[] args)
        {
            var ws = new WebServer(SendResponse, "http://localhost:3000/calculate/");
            ws.Run();
            Console.WriteLine("A simple webserver. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
        }
    }
}