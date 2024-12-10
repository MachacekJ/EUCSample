using EUC.Server.Storages.Models;

namespace EUC.Server.Storages;

public interface IRepository
{
  RepositoryInfo RepositoryInfo { get; }
  Task UpSchema();
}