using EUC.Server.Storages;
using EUC.Server.Storages.Contexts.EF.Models;

namespace EUC.Server.Modules.PersistSettingsModule.Repositories;

public interface ISettingsDbModuleRepository : IRepository
{
  Task<string?> Setting_GetAsync(string key, bool isRequired = true);
  Task<RepositoryOperationResult> Setting_SaveAsync(string key, string value, bool isSystem = false);
}