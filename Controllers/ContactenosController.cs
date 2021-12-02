using System.Data;
using APICafecito.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace APICafecito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactenosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ContactenosController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
			            SELECT id,
                        nombre,
                        email,
                        servicio,
                        mensaje
                        FROM contactenos;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        //CREACION
        [HttpPost]
        public JsonResult Post(Models.contactenos con)
        {
            string query = @"
                        INSERT INTO contactenos
                        (nombre,
                        email,
                        servicio,
                        mensaje)
                        VALUES
                        (@ContactenosNombre,
                        @ContactenosEmail,
                        @ContactenosServicio,
                        @ContactenosMensaje);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ContactenosId", con.id);
                    myCommand.Parameters.AddWithValue("ContactenosNombre", con.nombre);
                    myCommand.Parameters.AddWithValue("@ContactenosEmail", con.email);
                    myCommand.Parameters.AddWithValue("@ContactenosServicio", con.servicio);
                    myCommand.Parameters.AddWithValue("@ContactenosMensaje", con.mensaje);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        //Modificacion
        [HttpPut]
        public JsonResult Put(contactenos con)
        {
            string query = @"                       
                        UPDATE contactenos
                        SET
                        nombre = @ContactenosNombre,
                        email = @ContactenosEmail,
                        servicio = @ContactenosServicio,
                        mensaje = @ContactenosMensaje
                        WHERE id = @ContactenosId;

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ContactenosId", con.id);
                    myCommand.Parameters.AddWithValue("@ContactenosNombre", con.nombre);
                    myCommand.Parameters.AddWithValue("@ContactenosEmail", con.email);
                    myCommand.Parameters.AddWithValue("@ContactenosServicio", con.servicio);
                    myCommand.Parameters.AddWithValue("@ContactenosMensaje", con.mensaje);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        //ELIMINACION
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        DELETE FROM contactenos 
                        WHERE id=@ContactenosId;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ContactenosId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
