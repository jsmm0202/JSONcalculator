using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Currency_calculator.WebServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
//                endpoints.MapGet("/", async context =>
//                {
//                    await context.Response.WriteAsync(@"Welcome to the amazing calculator!
//To get your calculations, send a post request to http://localhost:3000/calculate in the next format:
//{'calculatorState': null, 'input': '1'}");
//                });
                endpoints.MapPost("/calculate", context => CalculateRequest(context));
            });
        }

        private static async Task CalculateRequest(HttpContext context)
        {
            string stringRequest = string.Empty;
            using (Stream body = context.Request.Body)
            {
                if (body != null)
                {
                    using (StreamReader reader = new StreamReader(body))
                    {
                        stringRequest = await reader.ReadToEndAsync();
                    }
                }
            }

            // Console.WriteLine("DEBUG: Request is - {0}", stringRequest); // Only for debug

            string input = JsonConvert.DeserializeObject<JsonRequest>(stringRequest).input;
            JsonState jsonCalculatorState = JsonConvert.DeserializeObject<JsonRequest>(stringRequest).calculatorState;

            var calculator = new Calculator();
            string jsonState;
            try
            {
                jsonState = calculator.CalculateNextState(jsonCalculatorState, input);
            }
            catch (Exception ex)
            {
                jsonState = ex.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.ContentLength = jsonState.Length;
            await context.Response.WriteAsync(jsonState);
        }
    }
}
