using EUC.Server.Modules.PersistSettingsModule.Repositories;
using EUC.Server.Storages.CQRS.Handlers;
using EUC.Server.Storages.CQRS.Handlers.Models;
using EUC.Server.Storages.Services.StorageResolvers;
using MediatR;

namespace EUC.Server.Modules.PersistSettingsModule.CQRS;

public abstract class SettingsDbModuleRequestHandler<TRequest, TResponse>(IStorageResolver storageResolver) :
  StorageRequestHandler<TRequest, TResponse>(storageResolver)
  where TRequest : IRequest<TResponse>
  where TResponse : Result
{
  protected Task<Result> StorageAction(Func<ISettingsDbModuleRepository, StorageExecutorItem> executor)
    => base.StorageParallelAction(executor);
}