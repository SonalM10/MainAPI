using MainAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainAPI.Services
{
    public interface IStates
    {
      List<State> GetStates();
      State UpdateState(State state);
      List<State> GetDetailsByID(State state);
    }
}
