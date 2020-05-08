using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Context;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace CourseLibrary.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setApplication =>
            {

                setApplication.ReturnHttpNotAcceptable = true;


            }).AddXmlDataContractSerializerFormatters()
            .AddNewtonsoftJson(setap => {
                setap.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            })
            .ConfigureApiBehaviorOptions(setUp =>
            {
                setUp.InvalidModelStateResponseFactory = context =>
                {
                    var probelDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "https:://Courselibrary.com/ModelValidationProblem",
                        Detail = "See the Error Property Fro More Detial",
                        Title = "one Or More Validation Error Orrur",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Instance = context.HttpContext.Request.Path
                    };

                    probelDetail.Extensions.Add("traceID", context.HttpContext.TraceIdentifier);

                    return new UnprocessableEntityObjectResult(probelDetail)
                    {
                        ContentTypes = { "APPLICATION/PROBLEM+JSON" }
                        };
                };

            });

            //  AuthoMapper Dependencies are declare here.. 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();


            services.AddDbContext<CourseLibraryContext>(options =>
            {
                options.UseSqlServer(
                    @"Server=DESKTOP-1TS4UA1\SQLEXPRESS; Database=CourseLibraryDB;Trusted_Connection=true"
                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Run When exception occur while development of the code.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // this will execute in the production environment

                app.UseExceptionHandler(appBuilder =>
                {

                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;

                        await context.Response.WriteAsync("An Unexpected fault happeneed, Try AGAIN!");
                    });
                });
            }



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
