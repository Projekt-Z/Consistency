﻿using Task = Web.Domain.Task;

namespace Web.Application.Tasks;

public interface ITasksRepository
{
    Task<IEnumerable<Task>?> GetAll(Guid ownerId);
}