using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Web.Domain;
using Web.Domain.DTOs;

namespace Web.Application.Identity;

public class IdentityRepository : IIdentityRepository
{
    private readonly HttpClient _httpClient;

    public IdentityRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<IEnumerable<User>?> GetAllUsers()
    {
        return _httpClient.GetFromJsonAsync<IEnumerable<User>>(_httpClient.BaseAddress + "auth");
    }

    public async Task<(bool, object)> RegisterUser(RegisterUserDto userDto)
    {
        var serializedUser = JsonConvert.SerializeObject(userDto);

        object? content;
        
        var requestMessage = new HttpRequestMessage(HttpMethod.Put, _httpClient.BaseAddress + "auth");
        requestMessage.Content = new StringContent(serializedUser);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);
        
        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.Created)
        {
            //content = JsonConvert.DeserializeObject<User>(responseBody);
            return (true, string.Empty);
        }

        content = JsonConvert.DeserializeObject<MessageResponseDto>(responseBody);
        return (false, content.ToString());
    }

    public async Task<(bool, object)> LoginUser(SignInRequestDto userDto)
    {
        var serializedUser = JsonConvert.SerializeObject(userDto);
        
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress + "auth");
        requestMessage.Content = new StringContent(serializedUser);
        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        
        var response = await _httpClient.SendAsync(requestMessage);
        
        var responseBody = await response.Content.ReadAsStringAsync();
        
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = JsonConvert.DeserializeObject<SignInResponseDto>(responseBody);
            return (true, content);
        }

        var error = JsonConvert.DeserializeObject<MessageResponseDto>(responseBody);
        return (false, error.ToString());
    }
}