using System;
using System.Collections.Generic;

namespace SwissVoting.DAL.DB
{
    public partial class Votes
    {
        public int PlaceId { get; set; }
        public int LawId { get; set; }
        public int For { get; set; }
        public int Against { get; set; }

        public virtual Laws Law { get; set; }
        public virtual Places Place { get; set; }
    }
}
