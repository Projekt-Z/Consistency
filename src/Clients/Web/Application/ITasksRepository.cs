using Task = Domain.Task;

namespace Application;

public interface ITasksRepository
{
    Task<IEnumerable<Task>?> GetAll(Guid ownerId);
}