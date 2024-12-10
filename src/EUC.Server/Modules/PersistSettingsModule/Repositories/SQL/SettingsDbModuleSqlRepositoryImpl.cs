﻿using EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.Models;
using EUC.Server.Storages;
using EUC.Server.Storages.Contexts.EF;
using EUC.Server.Storages.Contexts.EF.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EUC.Server.Modules.PersistSettingsModule.Repositories.SQL;

internal abstract class SettingsDbModuleSqlRepositoryImpl : DbContextBase, ISettingsDbModuleRepository
{
  private static readonly CacheKey CacheKeyTableSetting = CacheKey.Create(CacheCategories.Entity, nameof(SettingsEntity));
  private readonly IMediator _mediator;
  protected override string ModuleName => nameof(ISettingsDbModuleRepository);

  public DbSet<SettingsEntity> Settings { get; set; }

  #region Settings

  public async Task<string?> Setting_GetAsync(string key, bool isRequired = true)
    => (await GetSettingsAsync(key, isRequired))?.Value;

  public async Task<RepositoryOperationResult> Setting_SaveAsync(string key, string value, bool isSystem = false)
  {
    var set = await Settings.FirstOrDefaultAsync(i => i.Key == key)
              ?? new SettingsEntity
              {
                Key = key
              };

    set.Value = value;
    set.IsSystem = isSystem;

    var res =  await Save<SettingsEntity, int>(set);

    await _mediator.Send(new MemoryCacheModuleRemoveKeyCommand(CacheKeyTableSetting));
    return res;
  }

  private async Task<SettingsEntity?> GetSettingsAsync(string key, bool exceptedValue = true)
  {
    List<SettingsEntity>? allSettings;

    var allSettingsCacheResult = await _mediator.Send(new MemoryCacheModuleGetQuery(CacheKeyTableSetting));

    if (allSettingsCacheResult is { IsSuccess: true, ResultValue: not null })
    {
      if (allSettingsCacheResult.ResultValue.ObjectValue == null)
      {
        var ex = new Exception("The key '" + key + "' is not represented in settings table.");
        Logger.LogError("GetSettingsValue->" + key, ex);
        throw ex;
      }

      allSettings = allSettingsCacheResult.ResultValue.ObjectValue as List<SettingsEntity>;
    }
    else
    {
      allSettings = await Settings.ToListAsync();
      await _mediator.Send(new MemoryCacheModuleSaveCommand(CacheKeyTableSetting, allSettings));
    }

    if (allSettings == null)
      throw new ArgumentException($"{nameof(Settings)} entity table is null.");

    var vv = allSettings.FirstOrDefault(a => a.Key == key);
    if (vv == null && exceptedValue)
      throw new Exception($"Value for setting {nameof(key)} is not set. Check {nameof(Settings)} table.");

    return vv;
  }

  #endregion

  protected SettingsDbModuleSqlRepositoryImpl(DbContextOptions options, IMediator mediator, ILogger<SettingsDbModuleSqlRepositoryImpl> logger) : base(options, mediator, logger)
  {
    _mediator = mediator;
    RegisterDbSet(Settings);
  }
}