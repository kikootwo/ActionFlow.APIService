using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFlow.APIService.Models
{
    public class Customer
    {
        [Key]
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public eCustomerStatus Status { get; set; }
    }
}
