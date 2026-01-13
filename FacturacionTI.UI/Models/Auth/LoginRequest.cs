using System.ComponentModel.DataAnnotations;

namespace FacturacionTI.UI.Models.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El ruc es obligatorio")]
        public string Ruc { get; set; } = string.Empty;

        [Required(ErrorMessage = "El username es obligatorio")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
        public string Password { get; set; } = string.Empty;

        public bool Recordarme { get; set; }
    }
}
