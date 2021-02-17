using System;

namespace ActionFlow.API.Models
{
    public class Customer
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public eCustomerStatus Status { get; set; }
    }
}
