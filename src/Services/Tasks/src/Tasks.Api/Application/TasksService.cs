using MongoDB.Driver;
using Tasks.Api.Domain;

namespace Tasks.Api.Application;

public class TasksService : ITasksService
{
    private readonly IMongoCollection<TaskModel> _tasks;
    public TasksService(WebApplicationBuilder app)
    {
        var connectionString = app.Configuration.GetConnectionString("mongodb");
        //var mongoUrl = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(connectionString);
        var db = mongoClient.GetDatabase("ctaskdb");
        _tasks = db.GetCollection<TaskModel>("tasks");
    }
    
    public TaskModel Add(CreateTaskModelRequest request, out Guid taskId)
    {
        taskId = Guid.NewGuid();

        var task = new TaskModel
        {
            Content = request.Content,
            CreatedAt = DateTime.Now,
            Finished = false,
            OwnerId = request.OwnerId
        };

        _tasks.InsertOneAsync(task);

        return task;
    }

    public async Task<TaskModel?> Get(string id)
    {
        var definition = Builders<TaskModel>.Filter.Eq(x => x.Id, id);
        return await _tasks.Find(definition).SingleOrDefaultAsync();
    }

    public Task<List<TaskModel>> GetAll()
    {
        return _tasks.Find(Builders<TaskModel>.Filter.Empty).ToListAsync();
    }

    public async Task<List<TaskModel>> GetAllForUser(Guid ownerId)
    {
        var definition = Builders<TaskModel>.Filter.Eq(x => x.OwnerId, ownerId);
        return await _tasks.Find(definition).ToListAsync();
    }

    public async Task<bool> Delete(string taskId, Guid ownerId)
    {
        var definition = Builders<TaskModel>.Filter.Eq(x => x.Id, taskId);
        var task = _tasks.Find(definition).SingleOrDefaultAsync();

        if (task.Result is null) return false;

        if (task.Result.OwnerId != ownerId) return false;
        
        await _tasks.DeleteOneAsync(definition);

        return true;
    }

    public async Task<bool> Update(string taskId, CreateTaskModelRequest request)
    {
        var definition = Builders<TaskModel>.Filter.Eq(x => x.Id, taskId);
        var task = _tasks.Find(definition).SingleOrDefaultAsync();

        if (task.Result is null) return false;

        if (task.Result.OwnerId != request.OwnerId) return false;

        await _tasks.ReplaceOneAsync(definition, new TaskModel
        {
            Id = taskId,
            Content = request.Content,
            CreatedAt = task.Result.CreatedAt,
            Finished = task.Result.Finished,
            LastEdited = DateTime.Now,
            OwnerId = request.OwnerId
        });
        
        return true;
    }
}