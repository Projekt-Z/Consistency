using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Tasks.Domain;
using Web.Domain.DTOs;
using Web.Domain.DTOs.Tasks;
using Task = Web.Domain.Task;

namespace Web.Application.Tasks;

public class TasksRepository : ITasksRepository
{
    private readonly HttpClient _httpClient;
    
    public TasksRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public Task<IEnumerable<Task>?> GetAll(Guid ownerId)
    {
        return _httpClient.GetFromJsonAsync<IEnumerable<Task>>(_httpClient.BaseAddress + $"tasks/all/{ownerId}");
    }

    public async Task<(bool, object)> Add(AddTaskRequestDto requestDto)
    {
        var json = JsonConvert.SerializeObject(requestDto);

        object? content;
        
        var requestMessage = new HttpRequestMessage(HttpMethod.Put, _httpClient.BaseAddress + "tasks");
        requestMessage.Content = new StringContent(json);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);
        
        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.Created)
        {
            content = JsonConvert.DeserializeObject<Task>(responseBody);

            var task = content as Task;
            
            return (true, task.Id);
        }

        content = JsonConvert.DeserializeObject<MessageResponseDto>(responseBody);
        return (false, content.ToString());
    }

    public async Task<(bool, object)> Edit(EditTaskModelRequest requestDto, string taskId)
    {
        var json = JsonConvert.SerializeObject(requestDto);

        object? content;
        
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress + $"tasks/{taskId}");
        requestMessage.Content = new StringContent(json);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);
        
        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.OK)
        {
            //content = JsonConvert.DeserializeObject<User>(responseBody);
            return (true, string.Empty);
        }

        content = JsonConvert.DeserializeObject<MessageResponseDto>(responseBody);
        return (false, content.ToString());
    }

    public Task<Task?> Get(string id)
    {
        return _httpClient.GetFromJsonAsync<Task>(_httpClient.BaseAddress + $"tasks/{id}");
    }

    public async Task<(bool, object)> Delete(string taskId, Guid ownerId)
    {
        var requestDto = new DeleteTaskModelRequest
        {
            OwnerId = ownerId
        };
        
        var json = JsonConvert.SerializeObject(requestDto);

        object? content;
        
        var requestMessage = new HttpRequestMessage(HttpMethod.Delete, _httpClient.BaseAddress + $"tasks/{taskId}");
        requestMessage.Content = new StringContent(json);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var response = await _httpClient.SendAsync(requestMessage);
        
        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return (true, string.Empty);
        }

        content = JsonConvert.DeserializeObject<MessageResponseDto>(responseBody);
        return (false, content.ToString());
    }
}