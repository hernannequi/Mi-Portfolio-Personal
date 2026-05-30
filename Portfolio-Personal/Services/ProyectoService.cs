using Portfolio_Personal.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Portfolio_Personal.Services
{
    public class ProyectoService
    {
        private readonly IWebHostEnvironment _env;

        public ProyectoService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<Proyecto> ObtenerProyectos()
        {
            string path = Path.Combine(_env.WebRootPath, "data", "projects.json");

            if (!File.Exists(path))
                return new List<Proyecto>();

            var json = File.ReadAllText(path);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Proyecto> jsonDeserialized = JsonSerializer.Deserialize<List<Proyecto>>(json, options)
                   ?? new List<Proyecto>();
            
            return jsonDeserialized;
        }
    }
}
