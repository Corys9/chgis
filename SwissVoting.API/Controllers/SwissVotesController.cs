using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwissVoting.DAL;
using SwissVoting.Models;

namespace SwissVoting.API.Controllers
{
    [ApiController]
    [Route("votes")]
    public class SwissVotesController : ControllerBase
    {
        private readonly ILogger<SwissVotesController> _logger;
        private readonly ISwissVotingRepo _swissVotingRepo;

        public SwissVotesController(ILogger<SwissVotesController> logger, ISwissVotingRepo swissVotingRepo)
        {
            _logger = logger;
            _swissVotingRepo = swissVotingRepo;
        }

        [HttpGet]
        [Route("get-all-votes")]
        public List<VoteCount> GetAllVotes(int lawID) => _swissVotingRepo.GetVoteCounts(lawID);
    }
}
