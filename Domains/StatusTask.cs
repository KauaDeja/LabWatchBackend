using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class StatusTask
    {
        public StatusTask()
        {
            Tasks = new HashSet<Task>();
        }

        public int IdStatusTask { get; set; }
        public string StatusTaskE { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
