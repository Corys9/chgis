using System;

namespace SwissVoting.Models
{
    public class VoteCount
    {
        public int PlaceID { get; set; }
        public int For { get; set; }
        public int Against { get; set; }
    }
}
