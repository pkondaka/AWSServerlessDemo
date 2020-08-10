using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AWSServerlessDemo.Model;

namespace AWSServerlessDemo.Repositories
{
    public interface IDelayedFlightsRepository
    {
        Task<DelayedFlights> GetByIdAsync(string id);
        Task<IEnumerable<DelayedFlights>> GetByYearAsync(string year);

    }
}
