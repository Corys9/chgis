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
    [Route("laws")]
    public class LawsController : ControllerBase
    {
        private readonly ILogger<LawsController> _logger;
        private readonly ISwissVotingRepo _swissVotingRepo;

        public LawsController(
            ILogger<LawsController> logger,
            ISwissVotingRepo swissVotingRepo)
        {
            _logger = logger;
            _swissVotingRepo = swissVotingRepo;
        }

        [HttpGet]
        public List<Law> GetLaws()
            => _swissVotingRepo.GetLaws();
    }
}
