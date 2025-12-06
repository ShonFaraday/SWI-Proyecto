namespace MooneysV1.Models
{
    public class Libros
    {
        public int idLibro { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public string isbn { get; set; }
        public int stock { get; set; }
        public string url { get; set; }
        public string sinopsis { get; set; }
        public Libros() { }

    }
}
