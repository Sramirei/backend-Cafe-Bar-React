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
    public class PedidoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public PedidoController(IConfiguration configuration, IWebHostEnvironment env)
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
                        nombreCliente,
                        telefonoCliente,
                        emailCliente,
                        indicaciones,
                        productosCompra,
                        valorCompra,
                        estado
                        FROM pedido;
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
        public JsonResult Post(Models.Pedido pedi)
        {
            string query = @"
                        INSERT INTO pedido
                        (nombreCliente,
                        telefonoCliente,
                        emailCliente,
                        indicaciones,
                        valorCompra,
                        productosCompra,
                        estado)
                        VALUES
                        (@PedidoNombreCliente,
                        @PedidoTelefonoCliente,
                        @PedidoEmailCliente,
                        @PedidoIndicaciones,
                        @PedidoValorCompra,
                        @PedidoProductosCompra,
                        @PedidoEstado);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PedidoId", pedi.id);
                    myCommand.Parameters.AddWithValue("@PedidoNombreCliente", pedi.nombreCliente);
                    myCommand.Parameters.AddWithValue("@PedidoTelefonoCliente", pedi.telefonoCliente  );
                    myCommand.Parameters.AddWithValue("@PedidoEmailCliente", pedi.emailCliente  );
                    myCommand.Parameters.AddWithValue("@PedidoIndicaciones", pedi.indicaciones);
                    myCommand.Parameters.AddWithValue("@PedidoProductosCompra", pedi.productosCompra);
                    myCommand.Parameters.AddWithValue("@PedidoValorCompra", pedi.valorCompra);
                    myCommand.Parameters.AddWithValue("@PedidoEstado", pedi.estado);

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
        public JsonResult Put(Pedido pedi)
        {
            string query = @"                       
                        UPDATE pedido
                        SET
                        nombreCliente = @PedidoNombreCliente,
                        telefonoCliente = @PedidoTelefonoCliente,
                        emailCliente = @PedidoEmailCliente,
                        indicaciones = @PedidoIndicaciones ,
                        valorCompra = @PedidoValorCompra,
                        productosCompra = @PedidoProductosCompra,
                        estado = @PedidoEstado
                        WHERE id = @PedidoId;

            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PedidoId", pedi.id);
                    myCommand.Parameters.AddWithValue("@PedidoNombreCliente", pedi.nombreCliente);
                    myCommand.Parameters.AddWithValue("@PedidoTelefonoCliente", pedi.telefonoCliente  );
                    myCommand.Parameters.AddWithValue("@PedidoEmailCliente", pedi.emailCliente  );
                    myCommand.Parameters.AddWithValue("@PedidoIndicaciones", pedi.indicaciones);
                    myCommand.Parameters.AddWithValue("@PedidoProductosCompra", pedi.productosCompra);
                    myCommand.Parameters.AddWithValue("@PedidoValorCompra", pedi.valorCompra);

                    myCommand.Parameters.AddWithValue("@PedidoEstado", pedi.estado);

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
                        DELETE FROM pedido 
                        WHERE id=@PedidoId;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PedidoId", id);

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