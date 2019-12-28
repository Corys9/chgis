using SwissVoting.Models;
using System;
using System.Collections.Generic;

namespace SwissVoting.DAL
{
    public interface ISwissVotingRepo
    {
        List<VoteCount> GetVoteCounts(int lawID);
    }
}
