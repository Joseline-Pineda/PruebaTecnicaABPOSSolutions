namespace PruebaTecnicaABPOSSolutions.Models
{
    public class Menu
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public int NegocioId { get; set; }

        public Negocio Negocio { get; set; }

    }
}
