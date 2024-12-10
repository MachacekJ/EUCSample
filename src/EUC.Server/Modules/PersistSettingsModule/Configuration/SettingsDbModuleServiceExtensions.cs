using EUC.Server.Modules.PersistSettingsModule.CQRS;
using EUC.Server.Modules.PersistSettingsModule.Repositories;
using EUC.Server.Modules.PersistSettingsModule.Repositories.Mongo;
using EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.Memory;
using EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.PG;
using EUC.Server.Storages.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace EUC.Server.Modules.PersistSettingsModule.Configuration;

internal static class SettingsDbModuleServiceExtensions
{
  public static void AddSettingsDbModule(this IServiceCollection services, SettingsDbModuleOptions options)
  {
    services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(SettingsDbModulePipelineBehavior<,>));
    
    if (options.Storages == null)
      throw new ArgumentException($"{nameof(options.Storages)} is null.");
    
    services.AddDbMongoStorage<SettingsDbModuleMongoRepositoryImpl>(options.Storages);
    services.AddDbPGStorage<SettingsDbModuleSqlPGRepositoryImpl>(options.Storages);
    services.AddDbMemoryStorage<SettingsDbModuleSqlMemoryRepositoryImplTestsSqlMemoryRepositoryImpl>(options.Storages, nameof(ISettingsDbModuleRepository));
  }

  public static async Task UseSettingServiceModule(this IServiceProvider provider)
  {
    var opt = provider.GetService<IOptions<ACoreServerOptions>>()?.Value
              ?? throw new ArgumentException($"{nameof(SettingsDbModuleOptions)} is not configured.");

    StorageOptions? storageOptions = null;
    if (opt.DefaultStorages != null)
      storageOptions = opt.DefaultStorages;
    if (opt.SettingsDbModuleOptions.Storages != null)
      storageOptions = opt.SettingsDbModuleOptions.Storages;
    if (storageOptions == null)
      throw new ArgumentException($"{nameof(opt.SettingsDbModuleOptions)} is null. You can also use {nameof(opt.DefaultStorages)}.");

    await provider.ConfigureMongoStorage<ISettingsDbModuleRepository, SettingsDbModuleMongoRepositoryImpl>(storageOptions);
    await provider.ConfigurePGStorage<ISettingsDbModuleRepository, SettingsDbModuleSqlPGRepositoryImpl>(storageOptions);
    await provider.ConfigureMemoryStorage<ISettingsDbModuleRepository, SettingsDbModuleSqlMemoryRepositoryImplTestsSqlMemoryRepositoryImpl>(storageOptions);
  }
}