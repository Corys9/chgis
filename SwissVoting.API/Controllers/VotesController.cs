using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SwissVoting.DAL;
using SwissVoting.Models;

namespace SwissVoting.API.Controllers
{
    [ApiController]
    [Route("votes")]
    public class VotesController : ControllerBase
    {
        private readonly ILogger<VotesController> _logger;
        private readonly ISwissVotingRepo _swissVotingRepo;

        public VotesController(
            ILogger<VotesController> logger,
            ISwissVotingRepo swissVotingRepo)
        {
            _logger = logger;
            _swissVotingRepo = swissVotingRepo;
        }

        [HttpGet, Route("by-canton/{lawID:int}")]
        public Dictionary<int, VoteCount> GetVotesByCanton(int lawID)
            => _swissVotingRepo.GetVotesByCanton(lawID);

        [HttpGet, Route("custom/{lawID:int}")]
        public VoteCount GetVotesCustom(int lawID, string polystring)
            => _swissVotingRepo.GetVotesCustom(lawID, polystring);
    }
}
