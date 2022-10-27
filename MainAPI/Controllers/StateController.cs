using MainAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStates _states;

        public StateController(IStates states)
        {
            _states = states;
        }
        [HttpPost]
        [ActionName("GetState")]
        public IActionResult GetState()
        {
            var statename = _states.GetStates();
            return Ok(statename);
        }
    }
}
