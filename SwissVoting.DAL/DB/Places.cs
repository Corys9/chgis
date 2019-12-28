using System;
using System.Collections.Generic;

namespace SwissVoting.DAL.DB
{
    public partial class Places
    {
        public Places()
        {
            Votes = new HashSet<Votes>();
        }

        public int Gid { get; set; }
        public double? OsmId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Population { get; set; }

        public virtual ICollection<Votes> Votes { get; set; }
    }
}
