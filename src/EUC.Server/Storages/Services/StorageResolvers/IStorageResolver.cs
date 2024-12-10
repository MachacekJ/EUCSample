using EUC.Server.Storages.Definitions.Models;

namespace EUC.Server.Storages.Services.StorageResolvers;

public interface IStorageResolver
{
  Task ConfigureStorage<TStorage>(StorageImplementation implementation)
    where TStorage : IRepository;
  
  T ReadFromStorage<T>(StorageTypeEnum storageType = StorageTypeEnum.All) where T : IRepository;
  IEnumerable<T> WriteToStorages<T>(StorageTypeEnum storageType = StorageTypeEnum.All) where T : IRepository;
}