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
    }
}
