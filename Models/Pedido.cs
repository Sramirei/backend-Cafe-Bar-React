namespace APICafecito.Models
{
    public class Pedido
    {
        public int id { get; set; }
        public string nombreCliente { get; set; }
        public string telefonoCliente { get; set; }
        public string emailCliente { get; set; }
        public string indicaciones { get; set; }
        public string productosCompra { get; set; }
        public double valorCompra { get; set; }
        public string estado { get; set; }
    }
}
