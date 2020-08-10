using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AWSServerlessDemo.Model;

namespace AWSServerlessDemo.Repositories
{
    public class DelayedFlightsRepository : IDelayedFlightsRepository
    {
        private readonly DynamoDBContext _context;

        //private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        //private DynamoDBContext _context = new DynamoDBContext(client);

        public DelayedFlightsRepository(IAmazonDynamoDB dynamoDbClient)
        {
            if (dynamoDbClient == null) throw new ArgumentNullException(nameof(dynamoDbClient));
            this._context = new DynamoDBContext(new AmazonDynamoDBClient());
        }

        public async Task<DelayedFlights> GetByIdAsync(string id)
        {
            return await _context.LoadAsync<DelayedFlights>(id);
        }

        public async Task<IEnumerable<DelayedFlights>> GetByYearAsync(string year)
        {
            return await _context.QueryAsync<DelayedFlights>(year).GetRemainingAsync();
        }

    }
}
