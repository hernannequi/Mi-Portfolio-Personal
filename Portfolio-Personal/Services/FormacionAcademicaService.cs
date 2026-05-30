using Portfolio_Personal.Models;
using System.Text.Json;

namespace Portfolio_Personal.Services
{
    public class FormacionAcademicaService
    {
        private readonly IWebHostEnvironment _env;

        public FormacionAcademicaService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<FormacionAcademica> ObtenerFormacionesAcademicas()
        {
            string path = Path.Combine(_env.WebRootPath, "data", "studies.json");

            if (!File.Exists(path))
                return new List<FormacionAcademica>();

            var json = File.ReadAllText(path);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<FormacionAcademica> jsonDeserialized = JsonSerializer.Deserialize<List<FormacionAcademica>>(json, options)
                   ?? new List<FormacionAcademica>();

            return jsonDeserialized;
        }
    }
}
