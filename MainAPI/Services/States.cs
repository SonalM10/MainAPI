using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainAPI.Model;
using MainAPI.Services;
using MainAPI.DataContex;
using Dapper;
using System.Data;

namespace MainAPI.Services
{
    public class States : IStates
    {
        private readonly IDbContexts _dbContexts;
        DynamicParameters paras = null;

        public States(IDbContexts dbContexts)
        {
            _dbContexts= dbContexts;
        }
        public List<State> GetStates()
        {
            var result = new List<State>();
            try
            {
                paras = new DynamicParameters();
                string spName = "StateOperation";
                paras.Add("@Mode", "Select", System.Data.DbType.String);
                result = _dbContexts.ExecuteGetAll<State>(spName, paras);
                return result;
            }
            catch
            {
                return new List<State>();
            }
        }
        public State UpdateState(State state)
        {
            var result = new State();
            try
            {
                paras = new DynamicParameters();
                string spName = "StateOperation";
               
                if (state.StateId == 0)
                {
                    paras.Add("@Mode", "Insert", System.Data.DbType.String);                                       
                }
                else
                {
                    paras.Add("@Mode", "Update", System.Data.DbType.String);
                    paras.Add("@StateId", state.StateId.ToString(), System.Data.DbType.String);                                      
                }
                paras.Add("@StateName", state.Statename, System.Data.DbType.String);
                result = _dbContexts.Update<State>(spName, paras, CommandType.StoredProcedure);
                return result;
            }
            catch 
            {
                
                return new State ();
            }
        }
        public List<State> GetStateById(int StateId)
        {
            var result = new List<State>();
            try
            {
                paras = new DynamicParameters();
                string sSpName = "StateOperation";

                paras.Add("@Mode", "SelectById", System.Data.DbType.String);
                paras.Add("@StateId", StateId.ToString(), System.Data.DbType.String);
                result = _dbContexts.ExecuteGetAll<State>(sSpName, paras);
                return result;
            }
            catch (Exception ex)
            {            
                return new List<State>();
            }
        }

    

      
    }
}
