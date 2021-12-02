using System.Data;
using APICafecito.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace APICafecito.Controllers
{
    [Route("api/nosotros/[controller]")]
    [ApiController]
    public class InformacionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public InformacionController(IConfiguration configuration, IWebHostEnvironment env)
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
                        titulo,
                        contenido1,
                        contenido2
                        FROM informacion;
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
        public JsonResult Post(Models.Informacion info)
        {
            string query = @"
                        INSERT INTO informacion
                        (titulo,
                        contenido1,
                        contenido2)
                        VALUES
                        (@InformacionTitulo,
                        @InformacionContenido1,
                        @InformacionContenido2);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@InformacionId", info.id);
                    myCommand.Parameters.AddWithValue("@InformacionTitulo", info.titulo);
                    myCommand.Parameters.AddWithValue("@InformacionContenido1", info.contenido1);
                    myCommand.Parameters.AddWithValue("@InformacionContenido2", info.contenido2);

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
        public JsonResult Put(Informacion info)
        {
            string query = @"                       
                        UPDATE informacion
                        SET
                        titulo = @InformacionTitulo,
                        contenido1 = @InformacionContenido1,
                        contenido2 = @InformacionContenido2
                        WHERE id = @InformacionId;

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@InformacionId", info.id);
                    myCommand.Parameters.AddWithValue("@InformacionTitulo", info.titulo);
                    myCommand.Parameters.AddWithValue("@InformacionContenido1", info.contenido1);
                    myCommand.Parameters.AddWithValue("@InformacionContenido2", info.contenido2);

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
                        DELETE FROM informacion 
                        WHERE id=@InformacionId;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@InformacionId", id);

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
