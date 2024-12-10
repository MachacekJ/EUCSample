using EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.Models;
using EUC.Server.Storages.Contexts.EF.Scripts;
using EUC.Server.Storages.Definitions.EF;
using MediatR;
using Microsoft.Extensions.Logging;
using ScriptRegistrations = EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.PG.Scripts.ScriptRegistrations;

namespace EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.PG;

using ScriptRegistrations = ScriptRegistrations;

internal class SettingsDbModuleSqlPGRepositoryImpl(DbContextOptions<SettingsDbModuleSqlPGRepositoryImpl> options, IMediator mediator, ILogger<SettingsDbModuleSqlPGRepositoryImpl> logger) : SettingsDbModuleSqlRepositoryImpl(options, mediator, logger)
{
  protected override DbScriptBase UpdateScripts => new ScriptRegistrations();
  protected override EFStorageDefinition EFStorageDefinition => new PGStorageDefinition();
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<SettingsEntity>().HasKey(p => p.Id);
    
    SetDatabaseNames<SettingsEntity>(modelBuilder);
  }
  
  private static void SetDatabaseNames<T>(ModelBuilder modelBuilder) where T : class => SetDatabaseNames<T>(DefaultNames.ObjectNameMapping, modelBuilder);
}