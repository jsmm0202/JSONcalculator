using System;
using Newtonsoft.Json;

namespace Currency_calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var calculator = new Calculator();
            string jsonState = null;

            while (true)
            {
                Console.WriteLine("please enter an input");
                var input = Console.ReadLine();

                try
                {
                    jsonState = calculator.CalculateNextState(jsonState, input);
                    Console.WriteLine($"Displayed value: {JsonConvert.DeserializeObject<JsonState>(jsonState).Display}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
