using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Taaldc.Catalog.API.Controllers
{
    public class DBTesterController : ApiBaseController<DBTesterController>
    {
        private readonly IConfiguration _config;

        public DBTesterController(IConfiguration config)
        {
            _config = config;
        }  

        [HttpGet("connection-string")]
        public IActionResult GetConnectionString()
        {
            return Ok(_config["ConnectionStrings:DefaultConnection"]);
        }

        [HttpGet("check-connection")]
        public IActionResult CheckDbConnection()
        {
            var connString = _config["ConnectionStrings:DefaultConnection"];

            using (var l_oConnection = new SqlConnection(connString))
            {
                try
                {
                    l_oConnection.Open();
                    return Ok(true);
                }
                catch (SqlException err)
                {
                    return BadRequest(err.Message);
                }
            }
        }
    }
}
