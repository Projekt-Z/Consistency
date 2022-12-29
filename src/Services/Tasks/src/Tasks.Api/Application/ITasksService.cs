using Tasks.Api.Domain;

namespace Tasks.Api.Application;

public interface ITasksService
{
    void Add(TaskModel task);
    TaskModel? Get(Guid id);
    List<TaskModel> GetAll();
}