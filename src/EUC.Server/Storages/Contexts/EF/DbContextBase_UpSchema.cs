using EUC.Server.Modules.PersistSettingsModule.CQRS.SettingsDbGet;
using EUC.Server.Modules.PersistSettingsModule.CQRS.SettingsDbSave;
using EUC.Server.Modules.PersistSettingsModule.Repositories;
using EUC.Server.Storages.Contexts.EF.Scripts;
using EUC.Server.Storages.Definitions.Models;
using Microsoft.Extensions.Logging;

namespace EUC.Server.Storages.Contexts.EF;

public abstract partial class DbContextBase
{
  private bool _isDatabaseInit;
 // private string StorageVersionBaseSettingKey => $"StorageVersion_{Enum.GetName(typeof(StorageTypeEnum), StorageDefinition.Type)}_{nameof(ISettingsDbModuleStorage)}";
  private string StorageVersionKey => $"StorageVersion_{Enum.GetName(typeof(StorageTypeEnum), StorageDefinition.Type)}_{ModuleName}";

  public async Task UpSchema()
  {
    var allVersions = UpdateScripts.AllVersions.ToList();

    var lastVersion = new Version("0.0.0.0");

    // Get the latest implemented version, if any.
    _isDatabaseInit = await EFStorageDefinition.DatabaseHasInitUpdate(this, _options, mediator, Logger);
    if (!_isDatabaseInit)
    {
      var ver = await mediator.Send(new SettingsDbGetQuery(StorageDefinition.Type, StorageVersionKey));
      if (ver is { IsSuccess: true, ResultValue: not null })
        lastVersion = new Version(ver.ResultValue);
    }

    var updatedToVersion = new Version("0.0.0.0");

    if (allVersions.Count != 0)
    {
      if (EFStorageDefinition.IsTransactionEnabled)
      {
        await using var transaction = await Database.BeginTransactionAsync();
        try
        {
          updatedToVersion = await UpdateSchema(allVersions, lastVersion);
          await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
          await transaction.RollbackAsync();
          throw new Exception("UpdateDbAsync is rollback", ex);
        }
      }
      else
      {
        updatedToVersion = await UpdateSchema(allVersions, lastVersion);
      }
    }

    if (this is ISettingsDbModuleRepository aa)
    {
      await aa.Setting_SaveAsync(StorageVersionKey, updatedToVersion.ToString(), true);
      return;
    }

    await mediator.Send(new SettingsDbSaveCommand(StorageDefinition.Type, StorageVersionKey, updatedToVersion.ToString(), true));
  }

  private async Task<Version> UpdateSchema(List<DbVersionScriptsBase> allVersions, Version lastVersion)
  {
    var updatedToVersion = new Version("0.0.0.0");

    foreach (var version in allVersions.Where(a => a.Version > lastVersion))
    {
      foreach (var script in version.AllScripts)
      {
        var idTrans = Guid.NewGuid();
        try
        {
          Logger.LogInformation("START {transaction} update script version:{version}", idTrans, version.Version);
          Logger.LogInformation("SCRIPT {transaction}:{script}", idTrans, script);
          await Database.ExecuteSqlRawAsync(script);
          Logger.LogInformation("END {transaction} update script version:{version}", idTrans, version.Version);
        }
        catch (Exception ex)
        {
          Logger.LogCritical(ex, "ERROR {transaction} update script version:{version}", idTrans, version.Version);
          throw;
        }
      }

      version.AfterScriptRunCode(this, _options, mediator, Logger);
      updatedToVersion = version.Version;
    }

    return updatedToVersion;
  }
}