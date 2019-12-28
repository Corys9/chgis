using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SwissVoting.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwissVotesController : ControllerBase
    {
        private readonly ILogger<SwissVotesController> _logger;

        public SwissVotesController(ILogger<SwissVotesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Votes> Get()
        {
            
        }
    }
}
