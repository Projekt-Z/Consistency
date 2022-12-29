using Tasks.Api.Domain;

namespace Tasks.Api.Application;

public class TasksService : ITasksService
{
    public TasksService()
    {
        Tasks = new();
    }

    public List<TaskModel> Tasks { get; set; }
    
    public TaskModel Add(CreateTaskModelRequest request, out Guid taskId)
    {
        taskId = Guid.NewGuid();

        var task = new TaskModel
        {
            Id = taskId,
            Content = request.Content,
            CreatedAt = DateTime.Now,
            Finished = false,
            OwnerId = request.OwnerId
        };
        
        Tasks.Add(task);

        return task;
    }

    public TaskModel? Get(Guid id)
    {
        return Tasks.FirstOrDefault(x => x.Id == id);
    }

    public List<TaskModel> GetAll()
    {
        return Tasks;
    }

    public bool Delete(Guid taskId, Guid ownerId)
    {
        var task = Tasks.FirstOrDefault(x => x.Id == taskId);

        if (task is null) return false;

        if (task.OwnerId != ownerId) return false;
        
        var index = Tasks.IndexOf(task);
        Tasks.RemoveAt(index);
        return true;
    }

    public bool Update(Guid taskId, CreateTaskModelRequest request)
    {
        var task = Tasks.FirstOrDefault(x => x.Id == taskId);

        if (task is null) return false;
        
        if (task.OwnerId != request.OwnerId) return false;
        
        var index = Tasks.IndexOf(task);
        Tasks.RemoveAt(index);
        
        Tasks.Add(new TaskModel
        {
            Id = taskId,
            Content = request.Content,
            CreatedAt = task.CreatedAt,
            LastEdited = DateTime.Now,
            Finished = task.Finished,
            OwnerId = request.OwnerId
        });

        return true;
    }
}