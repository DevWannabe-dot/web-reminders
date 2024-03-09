using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ReminderWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderAppController : ControllerBase
    {
        private readonly IConfiguration _configuration; // não mudará uma vez inicializado

        public ReminderAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetReminders")]
        public IActionResult GetReminders()
        {
            string query = "SELECT * FROM dbo.reminders";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("reminderAppDBConnection");

            List<Dictionary<string, object>> remindersList = new List<Dictionary<string, object>>();

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    SqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                    // O bloco fecha myCommand ao final
                }
                // O bloco fecha myCon ao final
            }

            foreach (DataRow row in table.Rows)
            {
                var reminder = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    reminder[col.ColumnName] = row[col];
                }
                remindersList.Add(reminder);
            }
            /* O bloco de código acima recebe a tabela da base de dados, escreve cada linha em uma lista de dicionários e retorna na linha abaixo: */
            return Ok(remindersList);   // Código 200 (OK)
        }

        [HttpPost]
        [Route("Post_AddReminder")]
        public IActionResult Post_AddReminder([FromForm] string newReminderName, [FromForm] string newReminderDate)
        {
            string query = "INSERT INTO dbo.reminders VALUES(@newReminderName, @newReminderDate)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("reminderAppDBConnection");

            List<Dictionary<string, object>> remindersList = new List<Dictionary<string, object>>();

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.Add("@newReminderName", SqlDbType.NVarChar).Value = newReminderName;
                    myCommand.Parameters.Add("@newReminderDate", SqlDbType.Date).Value = newReminderDate;
                    SqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    // O bloco fecha myCommand ao final
                }
                // O bloco fecha myCon ao final
            }

            foreach (DataRow row in table.Rows)
            {
                var reminder = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    reminder[col.ColumnName] = row[col];
                }
                remindersList.Add(reminder);
            }
            /* O bloco de código acima recebe a tabela da base de dados, escreve cada linha em uma lista de dicionários e retorna na linha abaixo: */
            return new JsonResult(new { Success = true, Message = "Added Successfully"});
        }

        [HttpDelete]
        [Route("DeleteReminder")]
        public IActionResult DeleteReminder(int id)
        {
            string query = "DELETE FROM dbo.reminders WHERE id = @id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("reminderAppDBConnection");

            List<Dictionary<string, object>> remindersList = new List<Dictionary<string, object>>();

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    SqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    // O bloco fecha myCommand ao final
                }
                // O bloco fecha myCon ao final
            }

            foreach (DataRow row in table.Rows)
            {
                var reminder = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    reminder[col.ColumnName] = row[col];
                }
                remindersList.Add(reminder);
            }
            /* O bloco de código acima recebe a tabela da base de dados, escreve cada linha em uma lista de dicionários e retorna na linha abaixo: */
            return new JsonResult(new { Success = true, Message = "Deleted Successfully" });
        }
    }
}
