using Tasks.Api.Domain;

namespace Tasks.Api.Application;

public interface ITasksService
{
    TaskModel Add(CreateTaskModelRequest request, out Guid taskId);
    Task<TaskModel?> Get(string id);
    Task<List<TaskModel>> GetAll();
    Task<List<TaskModel>> GetAllForUser(Guid ownerId);
    Task<bool> Delete(string taskId, Guid ownerId);
    Task<bool> Update(string taskId, CreateTaskModelRequest request);
}