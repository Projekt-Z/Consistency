using System.Net.Http.Json;
using Task = Domain.Task;

namespace Application;

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
}