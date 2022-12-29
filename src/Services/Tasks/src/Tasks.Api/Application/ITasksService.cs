using Tasks.Api.Domain;

namespace Tasks.Api.Application;

public interface ITasksService
{
    TaskModel Add(CreateTaskModelRequest request, out Guid taskId);
    TaskModel? Get(Guid id);
    List<TaskModel> GetAll();
    bool Delete(Guid taskId, Guid ownerId);
    bool Update(Guid taskId, CreateTaskModelRequest request);
}