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
    public class EmpleadoController : ControllerBase{
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmpleadoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //CONSULTA
        [HttpGet]
        public JsonResult Get(){
            string query = @"
			            SELECT id,
                        nombre,
                        apellido,
                        cargo,
                        foto,
                        instagram,
                        youtube,
                        facebook,
                        twitter
                        FROM empleado;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource)) {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon)) {
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
        public JsonResult Post(Models.Empleado emp)
        {
            string query = @"
                        INSERT INTO empleado
                        (nombre,
                        apellido,
                        cargo,
                        foto,
                        instagram,
                        youtube,
                        facebook,
                        twitter)
                        VALUES
                        (@EmpleadoNombre,
                        @EmpleadoApellido,
                        @EmpleadoCargo,
                        @EmpleadoFoto,
                        @EmpleadoInstagram,
                        @EmpleadoYoutube,
                        @EmpleadoFacebook,
                        @EmpleadoTwitter);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@EmpleadoId", emp.id);
                    myCommand.Parameters.AddWithValue("@EmpleadoNombre", emp.nombre);
                    myCommand.Parameters.AddWithValue("@EmpleadoApellido", emp.apellido);
                    myCommand.Parameters.AddWithValue("@EmpleadoCargo", emp.cargo);
                    myCommand.Parameters.AddWithValue("@EmpleadoFoto", emp.foto);
                    myCommand.Parameters.AddWithValue("@EmpleadoInstagram", emp.instagram);
                    myCommand.Parameters.AddWithValue("@EmpleadoYoutube", emp.youtube);
                    myCommand.Parameters.AddWithValue("@EmpleadoFacebook", emp.facebook);
                    myCommand.Parameters.AddWithValue("@EmpleadoTwitter", emp.twitter);

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
        public JsonResult Put(Empleado emp)
        {
            string query = @"                       
                        UPDATE empleado
                        SET
                        nombre = @EmpleadoNombre,
                        apellido = @EmpleadoApellido,
                        cargo = @EmpleadoCargo,
                        foto = @EmpleadoFoto,
                        instagram = @EmpleadoInstagram,
                        youtube = @EmpleadoYoutube,
                        facebook = @EmpleadoFacebook,
                        twitter = @EmpleadoTwitter
                        WHERE id = @EmpleadoId;

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@EmpleadoId", emp.id);
                    myCommand.Parameters.AddWithValue("@EmpleadoNombre", emp.nombre);
                    myCommand.Parameters.AddWithValue("@EmpleadoApellido", emp.apellido);
                    myCommand.Parameters.AddWithValue("@EmpleadoCargo", emp.cargo);
                    myCommand.Parameters.AddWithValue("@EmpleadoFoto", emp.foto);
                    myCommand.Parameters.AddWithValue("@EmpleadoInstagram", emp.instagram);
                    myCommand.Parameters.AddWithValue("@EmpleadoYoutube", emp.youtube);
                    myCommand.Parameters.AddWithValue("@EmpleadoFacebook", emp.facebook);
                    myCommand.Parameters.AddWithValue("@EmpleadoTwitter", emp.twitter);

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
                        DELETE FROM empleado 
                        WHERE id=@EmpleadoId;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@EmpleadoId", id);

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