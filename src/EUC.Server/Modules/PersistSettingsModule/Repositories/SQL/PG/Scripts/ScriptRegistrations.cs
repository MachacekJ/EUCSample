﻿using EUC.Server.Storages.Contexts.EF.Scripts;

namespace EUC.Server.Modules.PersistSettingsModule.Repositories.SQL.PG.Scripts;

internal class ScriptRegistrations : DbScriptBase
{
    public override IEnumerable<DbVersionScriptsBase> AllVersions
    {
        get
        {
            var all = new List<DbVersionScriptsBase> { new V1_0_0_1SettingsTable() };
            return all;
        }
    }
}