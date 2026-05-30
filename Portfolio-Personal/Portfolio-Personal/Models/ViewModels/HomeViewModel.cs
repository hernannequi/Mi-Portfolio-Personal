namespace Portfolio_Personal.Models.ViewModels
{
    public class HomeViewModel
    {
        public FormularioContacto Contacto { get; set; } = new();
        public List<Proyecto> Proyectos { get; set; } = new();
        public List<ExperienciaLaboral> ExperienciasLaborales { get; set; } = new();
        public List<FormacionAcademica> FormacionesAcademicas { get; set; } = new();
    }
}
