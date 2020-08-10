using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AWSServerlessDemo.Model;

namespace AWSServerlessDemo.Services
{
    public interface IDelayedFlightsService
    {
        Task<DelayedFlights> GetByIdAsync(string id);
        Task<IEnumerable<DelayedFlights>> GetByYearAsync(string year);
    }
}
