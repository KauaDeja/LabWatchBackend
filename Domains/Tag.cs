using System;
using System.Collections.Generic;

#nullable disable

namespace labware_webapi.Domains
{
    public partial class Tag
    {
        public Tag()
        {
            Tasks = new HashSet<Task>();
        }

        public int IdTag { get; set; }
        public string TituloTag { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
