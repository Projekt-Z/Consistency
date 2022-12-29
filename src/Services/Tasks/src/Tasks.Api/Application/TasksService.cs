using Tasks.Api.Domain;

namespace Tasks.Api.Application;

public class TasksService : ITasksService
{
    public TasksService()
    {
        Tasks = new();
    }

    public List<TaskModel> Tasks { get; set; }
    
    public void Add(TaskModel task)
    {
        Tasks.Add(task);
    }

    public TaskModel? Get(Guid id)
    {
        return Tasks.FirstOrDefault(x => x.Id == id);
    }

    public List<TaskModel> GetAll()
    {
        return Tasks;
    }
}