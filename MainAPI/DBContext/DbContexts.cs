using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Hosting;
using System.Data.SqlClient;
using Microsoft.Extensions.Hosting;


namespace MainAPI.DataContex
{
    public class DbContexts : IDbContexts
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "StateDbContext";
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        private IDbConnection db;
        public DbContexts(IConfiguration config, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _config = config;
            env = hostingEnvironment;
            GetDbconnection();
        }

        public IDbConnection GetDbconnection()
        {
          
            Connectionstring = "StateDbContext";
            return db = new SqlConnection(_config.GetConnectionString(Connectionstring));
        }


        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }
        public async Task<T> ExecuteGetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();
            return (T)await db.QueryAsync<T>(sp, parms, commandType: commandType);
        }

        public async Task<int> ExecuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();
            return await db.ExecuteAsync(sp, parms, commandType: CommandType.StoredProcedure);
        }
        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();
            var result = db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
            return result;
        }
        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();
            var result = db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
            return result;
        }
        public async Task<int> ExecuteAsync<T>(string storedProcedureName, DynamicParameters parms, string dbName)
        {
            return await db.ExecuteAsync(
                           sql: storedProcedureName,
                           param: parms,
                           commandTimeout: null,
                           commandType: CommandType.StoredProcedure
                           );
        }

        public List<T> ExecuteGetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }
        public async Task<List<T>> ExecuteGetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();
            var result = await db.QueryAsync<T>(sp, parms, commandType: commandType);
            return result.ToList();
        }
        public async Task<T> ExecuteGet<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();
            var result = await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType);
            return result;
        }

    }
}
