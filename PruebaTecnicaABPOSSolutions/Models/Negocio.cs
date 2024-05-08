namespace PruebaTecnicaABPOSSolutions.Models
{
    public class Negocio
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
