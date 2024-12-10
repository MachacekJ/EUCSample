using System.Linq.Expressions;
using EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.Models;
using EUC.Server.Storages.Definitions.EF;

#pragma warning disable CS8603 // Possible null reference return.

namespace EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.PG;

public static class DefaultNames
{
  public static Dictionary<string, EFDbNames> ObjectNameMapping => new()
  {
    { nameof(SettingsEntity), new EFDbNames("setting", SettingsEntityColumnNames) },
  };

  private static Dictionary<Expression<Func<SettingsEntity, object>>, string> SettingsEntityColumnNames => new()
  {
    { e => e.Id, "setting_id" },
    { e => e.Key, "key" },
    { e => e.Value, "value" },
    { e => e.IsSystem, "is_system" }
  };

}