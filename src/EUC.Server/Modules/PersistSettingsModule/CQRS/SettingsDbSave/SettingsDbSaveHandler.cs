using EUC.Server.Storages.CQRS.Handlers.Models;
using EUC.Server.Storages.Services.StorageResolvers;

namespace EUC.Server.Modules.PersistSettingsModule.CQRS.SettingsDbSave;

public class SettingsDbSaveHandler(IStorageResolver storageResolver) : SettingsDbModuleRequestHandler<SettingsDbSaveCommand, Result>(storageResolver)
{
  public override async Task<Result> Handle(SettingsDbSaveCommand request, CancellationToken cancellationToken)
  {
    return await StorageAction((storage)
      => new StorageExecutorItem(storage.Setting_SaveAsync(request.Key, request.Value, request.IsSystem)));
  }
}