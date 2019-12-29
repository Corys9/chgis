using SwissVoting.Models;
using System;
using System.Collections.Generic;

namespace SwissVoting.DAL
{
    public interface ISwissVotingRepo
    {
        Dictionary<int, VoteCount> GetVotesByCanton(int lawID);

        List<Law> GetLaws();
    }
}
