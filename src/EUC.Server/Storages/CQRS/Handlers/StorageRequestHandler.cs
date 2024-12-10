﻿using System.Runtime.ExceptionServices;
using EUC.Server.Storages.Contexts.EF.Models;
using EUC.Server.Storages.CQRS.Handlers.Models;
using EUC.Server.Storages.CQRS.Results;
using EUC.Server.Storages.Services.StorageResolvers;
using MediatR;

namespace EUC.Server.Storages.CQRS.Handlers;

/// <summary>
/// For parallel saving/deleting data to/from storage.
/// </summary>
public abstract class StorageRequestHandler<TRequest, TResponse>(IStorageResolver storageResolver) : IRequestHandler<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
  where TResponse : Result
{
  public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

  protected async Task<Result> StorageEntityParallelAction<TStorage>(Func<TStorage, StorageEntityExecutorItem> executor, string sumHashSalt = "")
    where TStorage : IRepository
  {
    var allTask = storageResolver.WriteToStorages<TStorage>()
      .Select(executor).ToList();

    await WaitForAllParallelTasks(allTask.OfType<StorageExecutorItem>()
                              ?? throw new ArgumentNullException($"{nameof(allTask)}"));

    return EntityResult.SuccessWithEntityData(allTask);
  }
  
  protected async Task<Result> StorageParallelAction<TStorage>(Func<TStorage, StorageExecutorItem> executor)
    where TStorage : IRepository
  {
    var allTask = storageResolver.WriteToStorages<TStorage>()
      .Select(executor).ToList();

    await WaitForAllParallelTasks(allTask);

    return Result.Success();
  }
  
  private static async Task WaitForAllParallelTasks(IEnumerable<StorageExecutorItem> allTask)
  {
    Task<RepositoryOperationResult[]>? taskSum = null;
    try
    {
      taskSum = Task.WhenAll(allTask.Select(e => e.Task));
      await taskSum.ConfigureAwait(false);
    }
    catch
    {
      if (taskSum?.Exception != null) ExceptionDispatchInfo.Capture(taskSum.Exception).Throw();
      throw;
    }
  }
}