using FacturacionTI.UI.Models.Auth;

namespace FacturacionTI.UI.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task Logout();
        Task<string> GetToken();
        //Task<UsuarioDto> GetUsuarioActual();
    }
}
