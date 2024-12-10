using EUC.Server.Storages.Contexts.EF.Models;

namespace EUC.Server.Storages.CQRS.Handlers.Models;

public class StorageExecutorItem(Task<RepositoryOperationResult> task)
{
  public Task<RepositoryOperationResult> Task => task;
}