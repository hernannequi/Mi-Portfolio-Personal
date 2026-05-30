namespace Portfolio_Personal.Models
{
    public class Proyecto
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string GithubUrl { get; set; }
        public string DemoUrl { get; set; }
        public List<string> Tecnologias { get; set; }
    }
}
