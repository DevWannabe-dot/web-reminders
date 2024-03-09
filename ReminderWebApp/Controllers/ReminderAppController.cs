using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Security.AccessControl;

namespace ReminderWebApp.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderAppController : ControllerBase{
        private IConfiguration _configuration;

        public ReminderAppController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetReminders")]
        public JsonResult GetReminders() {
            String query = "SELECT * FROM reminders";
            DataTable table = new DataTable();
            String SqlDatasource = _configuration.GetConnectionString("reminderAppDBConnection");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(SqlDatasource)){
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)){
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    // Bloco using vai automaticamente fechar myCon
                }
            }

            return new JsonResult(table);
        }
    }
}
