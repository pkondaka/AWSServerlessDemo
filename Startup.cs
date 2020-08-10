using System.IO;
using System.Text.Json;
using Amazon.DynamoDBv2;
using AWSServerlessDemo.Extensions;
using AWSServerlessDemo.Repositories;
using AWSServerlessDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AWSServerlessDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IServiceCollection BuildContainer()
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddEnvironmentVariables()
              .Build();

            return ConfigureServices(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static IServiceCollection ConfigureServices(IConfigurationRoot configurationRoot)
        {
            var services = new ServiceCollection();
            services.AddControllers()
                .AddJsonOptions(x => { x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; });
            services.AddSingleton<IDelayedFlightsRepository, DelayedFlightsRepository>();
            services.AddSingleton<IDelayedFlightsService, DelayedFlightsService>();
            services.AddSwagger();
            //services.AddAWSService<IAmazonDynamoDB>(Configuration.GetAWSOptions("Dynamodb"));
            services.AddAWSService<AmazonDynamoDBClient>();

            return services;
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.AddCustomSwagger();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }

}
