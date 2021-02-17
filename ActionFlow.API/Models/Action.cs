using System;

namespace ActionFlow.API.Models
{
    public class Action
    {
        public Guid Guid { get; set; }
        public Guid? CustomerGuid { get; set; }
        public Guid? JobGuid { get; set; }
        public string ActionName { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
