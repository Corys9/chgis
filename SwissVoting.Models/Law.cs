using System;
using System.Collections.Generic;
using System.Text;

namespace SwissVoting.Models
{
    public class Law
    {
        public int ID { get; set; }

        public string Owner { get; set; }

        public string Proposal { get; set; }

        public DateTime FiledOn { get; set; }
    }
}
