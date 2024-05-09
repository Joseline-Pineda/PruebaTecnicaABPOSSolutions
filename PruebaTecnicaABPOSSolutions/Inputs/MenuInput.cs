namespace PruebaTecnicaABPOSSolutions.Inputs
{
    public class MenuInput
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int CategoriaId { get; set; }

        public int NegocioId { get; set; }
    }
}
