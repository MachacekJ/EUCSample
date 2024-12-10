using EUC.Server.Storages.Definitions.Models;

namespace EUC.Server.Storages.Definitions;

public abstract class StorageDefinition
{
  public abstract StorageTypeEnum Type { get; }
}