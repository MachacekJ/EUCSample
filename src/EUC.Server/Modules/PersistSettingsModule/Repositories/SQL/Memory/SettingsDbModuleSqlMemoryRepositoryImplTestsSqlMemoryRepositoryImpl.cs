using EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.Models;
using EUC.Server.Storages.Contexts.EF.Scripts;
using EUC.Server.Storages.Definitions.EF;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.Memory;

internal class SettingsDbModuleSqlMemoryRepositoryImplTestsSqlMemoryRepositoryImpl(DbContextOptions<SettingsDbModuleSqlMemoryRepositoryImplTestsSqlMemoryRepositoryImpl> options, IMediator mediator, ILogger<SettingsDbModuleSqlMemoryRepositoryImplTestsSqlMemoryRepositoryImpl> logger)
  : SettingsDbModuleSqlRepositoryImpl(options, mediator, logger)
{
  protected override DbScriptBase UpdateScripts => new ScriptRegistrations();
  protected override EFStorageDefinition EFStorageDefinition => new MemoryEFStorageDefinition();
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<SettingsEntity>().HasKey(p => p.Key);
  }
}