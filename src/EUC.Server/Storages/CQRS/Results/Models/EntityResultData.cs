using EUC.Server.Storages.Contexts.EF.Models;

namespace EUC.Server.Storages.CQRS.Results.Models;

public record EntityResultData(object PK, RepositoryOperationResult OperationResult);
