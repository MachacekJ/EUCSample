using EUC.Server.Storages.Contexts.EF.Scripts;

namespace EUC.Server.Modules.PersistSettingsModule.Repositories.Mongo.Scripts;

internal class ScriptRegistrations : DbScriptBase
{
    public override IEnumerable<DbVersionScriptsBase> AllVersions
    {
        get
        {
            var all = new List<DbVersionScriptsBase>
            {
                new V1_0_0_1SettingsCollection()
            };
            return all;
        }
    }
}