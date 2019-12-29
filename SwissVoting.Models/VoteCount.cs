using Newtonsoft.Json;
using System;

namespace SwissVoting.Models
{
    public class VoteCount
    {
        [JsonProperty("for")]
        public long For { get; set; }

        [JsonProperty("against")]
        public long Against { get; set; }
    }
}
