using System;
using System.Collections.Generic;

namespace SwissVoting.DAL.DB
{
    public partial class Laws
    {
        public Laws()
        {
            Votes = new HashSet<Votes>();
        }

        public int Id { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Proposal { get; set; }

        public virtual ICollection<Votes> Votes { get; set; }
    }
}
