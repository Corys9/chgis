using SwissVoting.DAL.DB;
using SwissVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwissVoting.DAL
{
    public class SwissVotingRepo : ISwissVotingRepo
    {
        public List<VoteCount> GetVoteCounts(int lawID)
        {
            using var db = new SwissContext();

            var voteCounts = from v in db.Votes
                             where v.LawId == lawID
                             select new VoteCount
                             {
                                 PlaceID = v.PlaceId,
                                 For = v.For,
                                 Against = v.Against
                             };

            return voteCounts.ToList();
        }
    }
}
