using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AWSServerlessDemo.Model;
using AWSServerlessDemo.Repositories;

namespace AWSServerlessDemo.Services
{
    public class DelayedFlightsService : IDelayedFlightsService
    {
        private readonly IDelayedFlightsRepository _delayedFlightsRepository;

        public DelayedFlightsService(IDelayedFlightsRepository delayedFlightsRepository)
        {
            _delayedFlightsRepository = delayedFlightsRepository ??
                                       throw new ArgumentNullException(nameof(delayedFlightsRepository));
        }

        public async Task<DelayedFlights> GetByIdAsync(string id)
        {
            var result = await _delayedFlightsRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<IEnumerable<DelayedFlights>> GetByYearAsync(string year)
        {
            var results = await _delayedFlightsRepository.GetByYearAsync(year);
            return results;
        }

    }
}
