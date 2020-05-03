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

            await context.Response.WriteAsync(jsonState);
        }
    }
}
