using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFlow.APIService.Models
{
    public class Job
    {
        [Key]
        public Guid Guid { get; set; }
        public string Description { get; set; }
        public Guid CustomerGuid { get; set; }
    }
}
