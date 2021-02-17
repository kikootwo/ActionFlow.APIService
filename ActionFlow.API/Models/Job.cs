using System;

namespace ActionFlow.API.Models
{
    public class Job
    {
        public Guid Guid { get; set; }
        public string Description { get; set; }
        public Guid CustomerGuid { get; set; }
    }
}
