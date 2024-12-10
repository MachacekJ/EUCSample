using EUC.Server.Modules.PersistSettingsModule.Repositories;
using EUC.Server.Storages.Configuration;

namespace EUC.Server.Modules.PersistSettingsModule.Configuration;

public class SettingsDbModuleOptionsBuilder : StorageModuleOptionBuilder
{

  public static SettingsDbModuleOptionsBuilder Empty() => new();


  public SettingsDbModuleOptions Build(StorageOptionBuilder? defaultStorages)
  {
    return new SettingsDbModuleOptions(IsActive)
    {
      Storages = BuildStorage(defaultStorages, nameof(ISettingsDbModuleRepository)),
    };
  }
}