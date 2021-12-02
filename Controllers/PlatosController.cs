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
    public class PlatosController : ControllerBase
    {
        
            private readonly IConfiguration _configuration;
            private readonly IWebHostEnvironment _env;
            public PlatosController(IConfiguration configuration, IWebHostEnvironment env)
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
                        title,
                        descripcion,
                        imgsrc,
                        precio
                        FROM plato;
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
            public JsonResult Post(Models.Platos emp)
            {
                string query = @"
                        INSERT INTO plato
                        (title,
                        descripcion,
                        imgsrc,
                        precio)
                        VALUES
                        (@PlatoTitle,
                        @PlatoDescripcion,
                        @PlatoImgsrc,
                        @PlatoPrecio);
                        
            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
                MySqlDataReader myReader;
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@PlatoId", emp.id);
                        myCommand.Parameters.AddWithValue("@PlatoTitle", emp.title);
                        myCommand.Parameters.AddWithValue("@PlatoDescripcion", emp.descripcion);
                        myCommand.Parameters.AddWithValue("@PlatoImgsrc", emp.imgsrc);
                        myCommand.Parameters.AddWithValue("@PlatoPrecio", emp.precio);

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
            public JsonResult Put(Platos emp)
            {
                string query = @"                       
                        UPDATE plato
                        SET
                        title = @PlatoTitle,
                        descripcion = @PlatoDescripcion,
                        imgsrc = @PlatoImgsrc,
                        precio = @PlatoPrecio
                        WHERE id = @PlatoId;

            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
                MySqlDataReader myReader;
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                    myCommand.Parameters.AddWithValue("@PlatoId", emp.id);
                    myCommand.Parameters.AddWithValue("@PlatoTitle", emp.title);
                    myCommand.Parameters.AddWithValue("@PlatoDescripcion", emp.descripcion);
                    myCommand.Parameters.AddWithValue("@PlatoImgsrc", emp.imgsrc);
                    myCommand.Parameters.AddWithValue("@PlatoPrecio", emp.precio);

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
                        DELETE FROM plato
                        WHERE id=@PlatoId;
            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
                MySqlDataReader myReader;
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@PlatoId", id);

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
