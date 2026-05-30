using System.ComponentModel.DataAnnotations;

namespace Portfolio_Personal.Models
{
    public class FormularioContacto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Mensaje { get; set; } = string.Empty;
    }
}
