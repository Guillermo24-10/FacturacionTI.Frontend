using FacturacionTI.UI.Models;
using FacturacionTI.UI.Models.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace FacturacionTI.UI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsruntime;
        private readonly AuthenticationStateProvider _authStateProvider;

        private const string TOKEN_KEY = "authToken";
        private const string USER_KEY = "userData";

        public AuthService(HttpClient httpClient, IJSRuntime jsruntime, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _jsruntime = jsruntime;
            _authStateProvider = authStateProvider;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            // llamar a la api
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", request);

            if (!response.IsSuccessStatusCode)
                return null!;

            var loginResponse = await response.Content.ReadFromJsonAsync<BaseResponse<LoginResponse>>();

            if (loginResponse == null)
                return null!;

            // guardar token en localstorage
            await _jsruntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, loginResponse.Data.Token);
            await _jsruntime.InvokeVoidAsync("localStorage.setItem", USER_KEY, JsonSerializer.Serialize(loginResponse.Data.Username));

            // notificar al AuthStateProvider
            ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(loginResponse.Data.Token);

            // configurar header para futuras peticiones

            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResponse.Data.Token);

            return loginResponse.Data;
        }

        public async Task Logout()
        {
            await _jsruntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
            await _jsruntime.InvokeVoidAsync("localStorage.removeItem", USER_KEY);

            ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
        }

        public async Task<string> GetToken()
        {
            return await _jsruntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
        }
    }
}
