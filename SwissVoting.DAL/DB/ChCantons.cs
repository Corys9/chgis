using System;
using System.Collections.Generic;

namespace SwissVoting.DAL.DB
{
    public partial class ChCantons
    {
        public int Gid { get; set; }
        public long? Id0 { get; set; }
        public string Iso { get; set; }
        public string Name0 { get; set; }
        public long? Id1 { get; set; }
        public string Name1 { get; set; }
        public string Type1 { get; set; }
        public string Engtype1 { get; set; }
        public string NlName1 { get; set; }
        public string Varname1 { get; set; }
    }
}
