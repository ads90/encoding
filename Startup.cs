using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2
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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                    var fs = new FileStream(_filePath + "/records.bin", FileMode.Open);
                    var arabic = Encoding.GetEncoding(1256);
                    var len = (int)fs.Length;
                    var bits = new byte[len];
                    fs.Read(bits, 0, len);
                    var result = arabic.GetString(bits);

                    context.Response.ContentType = "text/plain; charset=utf-8";
                    await context.Response.WriteAsync(result);
                });
            });
        }
    }
}
