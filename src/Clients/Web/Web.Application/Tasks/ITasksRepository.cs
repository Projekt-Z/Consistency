using Tasks.Domain;
using Web.Domain.DTOs.Tasks;
using Task = Web.Domain.Task;

namespace Web.Application.Tasks;

public interface ITasksRepository
{
    Task<IEnumerable<Task>?> GetAll(Guid ownerId);
    Task<(bool, object)> Add(AddTaskRequestDto requestDto);
    Task<(bool, object)> Edit(EditTaskModelRequest requestDto, string taskId);
    Task<Task?> Get(string id);
    Task<(bool, object)> Delete(string taskId, Guid ownerId);
}