namespace APICafecito.Models
{
    public class Reserva
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string documento { get; set; }
        public string telefono { get; set; }
        public int personas { get; set; }
        public string servicio { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string mensaje { get; set; }
        public string estado { get; set; }
    }
}
