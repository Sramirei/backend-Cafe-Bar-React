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
    public class TestimonioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public TestimonioController(IConfiguration configuration, IWebHostEnvironment env)
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
                        descripcion,
                        comentario,
                        foto
                        FROM testimonio;
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
        public JsonResult Post(Models.Testimonio testi)
        {
            string query = @"
                        INSERT INTO testimonio
                        (nombre,
                        descripcion,
                        comentario,
                        foto)
                        VALUES
                        (@TestimonioNombre,
                        @TestimonioDescripcion,
                        @TestimonioComentario,
                        @TestimonioFoto);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@TestimonioId", testi.id);
                    myCommand.Parameters.AddWithValue("@TestimonioNombre", testi.nombre);
                    myCommand.Parameters.AddWithValue("@TestimonioDescripcion", testi.descripcion);
                    myCommand.Parameters.AddWithValue("@TestimonioComentario", testi.comentario);
                    myCommand.Parameters.AddWithValue("@TestimonioFoto", testi.foto);
  
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
        public JsonResult Put(Testimonio testi)
        {
            string query = @"                       
                        UPDATE testimonio
                        SET
                        nombre = @TestimonioNombre,
                        descripcion = @TestimonioDescripcion,
                        comentario = @TestimonioComentario,
                        foto = @TestimonioFoto
                        WHERE id = @TestimonioId;

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@TestimonioId", testi.id);
                    myCommand.Parameters.AddWithValue("@TestimonioNombre", testi.nombre);
                    myCommand.Parameters.AddWithValue("@TestimonioDescripcion", testi.descripcion);
                    myCommand.Parameters.AddWithValue("@TestimonioComentario", testi.comentario);
                    myCommand.Parameters.AddWithValue("@TestimonioFoto", testi.foto);

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
                        DELETE FROM testimonio 
                        WHERE id=@TestimonioId;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@TestimonioId", id);

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
