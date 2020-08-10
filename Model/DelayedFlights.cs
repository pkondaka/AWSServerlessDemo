using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace AWSServerlessDemo.Model
{
    [DynamoDBTable("DelayedFlights")]
    public class DelayedFlights
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string DayofMonth { get; set; }
        public string DayOfWeek { get; set; }
        public string DepTime { get; set; }
        public string CRSDepTime { get; set; }
        public string ArrTime { get; set; }
        public string CRSArrTime { get; set; }
        public string UniqueCarrier { get; set; }
        public string FlightNum { get; set; }
        public string TailNum { get; set; }
        public string ActualElapsedTime { get; set; }
        public string CRSElapsedTime { get; set; }
        public string AirTime { get; set; }
        public string ArrDelay { get; set; }
        public string DepDelay { get; set; }
        public string Origin { get; set; }
        public string Dest { get; set; }
        public string Distance { get; set; }
    }
}
