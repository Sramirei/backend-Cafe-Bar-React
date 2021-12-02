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
    public class ServicioController : ControllerBase{
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ServicioController(IConfiguration configuration, IWebHostEnvironment env)
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
                        numero,
                        titulo,
                        descripcion,
                        foto
                        FROM servicio;
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
        public JsonResult Post(Models.Servicio ser)
        {
            string query = @"
                        INSERT INTO servicio
                        (numero,
                        titulo,
                        descripcion,
                        foto)
                        VALUES
                        (@ServicioNumero,
                        @ServicioTitulo,
                        @ServicioDescripcion,
                        @ServicioFoto);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ServicioId", ser.id);
                    myCommand.Parameters.AddWithValue("@ServicioNumero", ser.numero);
                    myCommand.Parameters.AddWithValue("@ServicioTitulo", ser.titulo);
                    myCommand.Parameters.AddWithValue("@ServicioDescripcion", ser.descripcion);
                    myCommand.Parameters.AddWithValue("@ServicioFoto", ser.foto);


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
        public JsonResult Put(Servicio ser)
        {
            string query = @"                       
                        UPDATE servicio
                        SET
                        numero = @ServicioNumero,
                        titulo = @ServicioTitulo,
                        descripcion = @ServicioDescripcion,
                        foto = @ServicioFoto
                        WHERE id = @ServicioId;

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ServicioId", ser.id);
                    myCommand.Parameters.AddWithValue("@ServicioNumero", ser.numero);
                    myCommand.Parameters.AddWithValue("@ServicioTitulo", ser.titulo);
                    myCommand.Parameters.AddWithValue("@ServicioDescripcion", ser.descripcion);
                    myCommand.Parameters.AddWithValue("@ServicioFoto", ser.foto);
                    

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
                        DELETE FROM servicio 
                        WHERE id=@ServicioId;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ServicioId", id);

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
