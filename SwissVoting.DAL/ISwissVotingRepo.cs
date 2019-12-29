using SwissVoting.Models;
using System;
using System.Collections.Generic;

namespace SwissVoting.DAL
{
    public interface ISwissVotingRepo
    {
        #region Votes
        Dictionary<int, VoteCount> GetVotesByCanton(int lawID);

        VoteCount GetVotesCustom(int lawID, string polystring);
        #endregion

        #region Laws
        List<Law> GetLaws();
        #endregion
    }
}
