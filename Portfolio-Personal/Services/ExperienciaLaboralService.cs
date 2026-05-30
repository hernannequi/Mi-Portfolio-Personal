using Portfolio_Personal.Models;
using System.Text.Json;

namespace Portfolio_Personal.Services
{
    public class ExperienciaLaboralService
    {
        private readonly IWebHostEnvironment _env;

        public ExperienciaLaboralService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<ExperienciaLaboral> ObtenerExperienciasLaborales()
        {
            string path = Path.Combine(_env.WebRootPath, "data", "work-experience.json");

            if (!File.Exists(path))
                return new List<ExperienciaLaboral>();

            var json = File.ReadAllText(path);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<ExperienciaLaboral> jsonDeserialized = JsonSerializer.Deserialize<List<ExperienciaLaboral>>(json, options)
                   ?? new List<ExperienciaLaboral>();

            return jsonDeserialized;
        }
    }
}
