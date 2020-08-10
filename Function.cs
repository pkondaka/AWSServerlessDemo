using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using AWSServerlessDemo.Model;
using AWSServerlessDemo.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSServerlessDemo
{
	public class Functions
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private readonly IDelayedFlightsService _delayedFlightsService;

        IAmazonDynamoDB _client { get; set; }

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions(/*IDelayedFlightsService delayedFlightsService*/)
        {
            //this._delayedFlightsService = delayedFlightsService;
        }


        /// <summary>
        /// A Lambda function to respond to HTTP Get methods from API Gateway
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The API Gateway response.</returns>
        public async Task<APIGatewayProxyResponse> GetFlightDelaysById(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string id = request.PathParameters["Id"];
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            try
            {
                DynamoDBContext dbContext = new DynamoDBContext(client);
                //int transactionId = 2;
                var dbResult = await dbContext.LoadAsync<DelayedFlights>(id);
                //var dbResult = await _delayedFlightsService.GetByIdAsync(id);
                string jsonString = System.Text.Json.JsonSerializer.Serialize<DelayedFlights>(dbResult);
                Console.WriteLine(jsonString);
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = JsonConvert.SerializeObject(dbResult),
                    Headers = new Dictionary<string, string>
                                {
                                    { "Content-Type", "application/json" },
                                    { "Access-Control-Allow-Origin", "*" }
                                }
                };

            }
            catch (Exception e)
            {
                LambdaLogger.Log("Error connecting: " + e.Message);
                LambdaLogger.Log(e.StackTrace);
                return new APIGatewayProxyResponse
                {
                    StatusCode = 500,
                    Body = $"Failed to connecting: {e.Message}"
                };
            }
        }

        public async Task<APIGatewayProxyResponse> GetFlightDelaysByYear(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string year = request.PathParameters["Year"];
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            try
            {
                DynamoDBContext dbContext = new DynamoDBContext(client);
                //int transactionId = 2;
                var dbResult = await dbContext.LoadAsync<DelayedFlights>(year);
                string jsonString = System.Text.Json.JsonSerializer.Serialize<DelayedFlights>(dbResult);
                Console.WriteLine(jsonString);
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = JsonConvert.SerializeObject(dbResult),
                    Headers = new Dictionary<string, string>
                                {
                                    { "Content-Type", "application/json" },
                                    { "Access-Control-Allow-Origin", "*" }
                                }
                };

            }
            catch (Exception e)
            {
                LambdaLogger.Log("Error connecting: " + e.Message);
                LambdaLogger.Log(e.StackTrace);
                return new APIGatewayProxyResponse
                {
                    StatusCode = 500,
                    Body = $"Failed to connecting: {e.Message}"
                };
            }
        }

    }
}
