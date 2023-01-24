using Web.Domain.DTOs.Tasks;
using Task = Web.Domain.Task;

namespace Web.Application.Tasks;

public interface ITasksRepository
{
    Task<IEnumerable<Task>?> GetAll(Guid ownerId);
    Task<(bool, object)> Add(AddTaskRequestDto requestDto);
    Task<(bool, object)> Delete(string taskId, Guid ownerId);
}