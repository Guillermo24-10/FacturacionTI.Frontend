using System.ComponentModel.DataAnnotations;

namespace FacturacionTI.UI.Models.Auth
{
    public class ResetPasswordRequest
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La nueva contraseña debe tener al menos 6 caracteres.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
