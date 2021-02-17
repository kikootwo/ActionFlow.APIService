using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFlow.APIService.Models
{
    public class Action
    {
        [Key]
        public Guid Guid { get; set; }
        public Guid? CustomerGuid { get; set; }
        public Guid? JobGuid { get; set; }
        public string ActionName { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
