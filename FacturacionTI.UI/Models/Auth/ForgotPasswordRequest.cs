using System.ComponentModel.DataAnnotations;

namespace FacturacionTI.UI.Models.Auth
{
    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "Campo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido")]
        public string Email { get; set; } = string.Empty;
    }
}
