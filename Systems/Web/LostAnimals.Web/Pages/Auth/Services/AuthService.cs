using System.Net.Http.Headers;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using LostAnimals.Web.Pages.Auth.Models;
using LostAnimals.Web.Providers;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace LostAnimals.Web.Pages.Auth.Services;

public class AuthService : IAuthService
{
    private const string LocalStorageAuthTokenKey = "authToken";
    private const string LocalStorageRefreshTokenKey = "refreshToken";

    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<Result> ConfirmEmailAsync(string email, string token)
    {
        var response = await _httpClient.GetAsync($"v1/accounts/confirmemail?token={token}&email={email}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Result>();
            return result;
        }
        else
        {
            return new Result(false, new[] { "Email confirmation failed." });
        }
    }


    public async Task<bool> IsAccountConfirmed(LoginModel model)
    {
        var response = await _httpClient.GetAsync($"v1/accounts/checkaccount/{model.Username}");
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                var userResponse = await _httpClient.GetAsync($"v1/accounts/{model.Username}");
                var user = await userResponse.Content.ReadFromJsonAsync<UserAccountViewModel>();
                if (user != null)
                {
                    await _httpClient.GetAsync($"v1/accounts/SendConfirmationLink/{user.Id}");
                    return false;
                }
            }
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return true;
    }

    public async Task<UserAccountViewModel> Register(RegisterModel registerModel)
    {
        var httpContent = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"v1/accounts/", httpContent);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<UserAccountViewModel>() ?? new();
    }


    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var url = $"{Settings.IdentityRoot}/connect/token";

        var request_body = new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("username", loginModel.Username!),
            new KeyValuePair<string, string>("password", loginModel.Password!)
        };

        var requestContent = new FormUrlEncodedContent(request_body);

        var response = await _httpClient.PostAsync(url, requestContent);

        var content = await response.Content.ReadAsStringAsync();

        var loginResult = System.Text.Json.JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
        loginResult.Successful = response.IsSuccessStatusCode;

        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await _localStorage.SetItemAsync(LocalStorageAuthTokenKey, loginResult.AccessToken);
        await _localStorage.SetItemAsync(LocalStorageRefreshTokenKey, loginResult.RefreshToken);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Username!);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

        return loginResult;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(LocalStorageAuthTokenKey);
        await _localStorage.RemoveItemAsync(LocalStorageRefreshTokenKey);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}