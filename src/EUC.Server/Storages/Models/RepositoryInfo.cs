using EUC.Server.Storages.Definitions;

namespace EUC.Server.Storages.Models;

public record RepositoryInfo(string ModuleName, StorageDefinition StorageDefinition);
