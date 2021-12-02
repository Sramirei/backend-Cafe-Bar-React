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
    public class HistoriaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public HistoriaController(IConfiguration configuration, IWebHostEnvironment env)
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
                        descripcion,
                        contenido,
                        background,
                        imagen
                        FROM historia;
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
        public JsonResult Post(Models.Historia hist)
        {
            string query = @"
                        INSERT INTO historia
                        (titulo,
                        descripcion,
                        contenido,
                        background,
                        imagen)
                        VALUES
                        (@HistoriaTitulo,
                        @HistoriaDescripcion,
                        @HistoriaContenido,
                        @HistoriaBackground,
                        @HistoriaImagen);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@HistoriaId", hist.id);
                    myCommand.Parameters.AddWithValue("@HistoriaTitulo", hist.titulo);
                    myCommand.Parameters.AddWithValue("@HistoriaDescripcion", hist.descripcion);
                    myCommand.Parameters.AddWithValue("@HistoriaContenido", hist.contenido);
                    myCommand.Parameters.AddWithValue("@HistoriaBackground", hist.background);
                    myCommand.Parameters.AddWithValue("@HistoriaImagen", hist.imagen);

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
        public JsonResult Put(Historia hist)
        {
            string query = @"                       
                        UPDATE historia
                        SET
                        titulo = @HistoriaTitulo,
                        descripcion = @HistoriaDescripcion,
                        contenido = @HistoriaContenido,
                        background = @HistoriaBackground,
                        imagen = @HistoriaImagen
                        WHERE id = @HistoriaId;

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@HistoriaId", hist.id);
                    myCommand.Parameters.AddWithValue("@HistoriaTitulo", hist.titulo);
                    myCommand.Parameters.AddWithValue("@HistoriaDescripcion", hist.descripcion);
                    myCommand.Parameters.AddWithValue("@HistoriaContenido", hist.contenido);
                    myCommand.Parameters.AddWithValue("@HistoriaBackground", hist.background);
                    myCommand.Parameters.AddWithValue("@HistoriaImagen", hist.imagen);

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
                        DELETE FROM historia 
                        WHERE id=@HistoriaId;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@HistoriaId", id);

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
