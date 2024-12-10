namespace EUC.Server.Modules.PersistSettingsModule.Configuration;

public class SettingsDbModuleOptions(bool isActive = false) : StorageModuleOptions(nameof(SettingsDbModule), isActive);