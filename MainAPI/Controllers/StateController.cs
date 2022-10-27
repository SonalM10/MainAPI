using MainAPI.Model;
using MainAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        [HttpPost("StateList")]      
        public IEnumerable<State> GetState()
        {
       
            return _states.GetStates();
           
        }
        [HttpPost("UpdateState")]
        public ActionResult<State> CreateEmployee(State state)
        {
            string Msg;
            try
            {
                if (state == null)
                {
                    return BadRequest();
                }
                else
                {
                    var result = _states.UpdateState(state);
                    Msg = "Sucessfully Updated";
                    return Ok( Msg);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating State");
            }
        }
        [HttpPost("StateById")]
        public IEnumerable<State> GetStateById(int StateId)
        {
            return _states.GetStateById(StateId);
        }
    }
}
