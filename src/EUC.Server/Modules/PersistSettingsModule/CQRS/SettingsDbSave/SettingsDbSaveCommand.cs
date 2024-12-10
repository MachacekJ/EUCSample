using EUC.Server.Storages.Definitions.Models;

namespace EUC.Server.Modules.PersistSettingsModule.CQRS.SettingsDbSave;

public class SettingsDbSaveCommand(StorageTypeEnum storageType, string key, string value, bool isSystem = false) : SettingsDbModuleRequest<Result>
{
  public StorageTypeEnum StorageType { get; } = storageType;
  public string Key { get; } = key;
  public string Value { get; } = value;
  public bool IsSystem { get; } = isSystem;

  public SettingsDbSaveCommand(string key, string value, bool isSystem = false) : this(StorageTypeEnum.All, key, value, isSystem)
  {
  }
}