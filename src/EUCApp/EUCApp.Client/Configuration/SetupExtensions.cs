using EUCApp.Client.UI.Services.App;
using EUCApp.Client.UI.Services.Page.Implementations;
using EUCApp.Client.UI.Services.Page.Interfaces;

namespace EUCApp.Client.Configuration;

public static class SetupExtensions
{
  public static void AddEUCSharedConfiguration(this IServiceCollection services)
  {
    var appConfig = new AppPagesConfiguration();
    services.AddSingleton<IAppStartConfiguration>(appConfig);
    
    services.AddSingleton<IAppState, AppState>();
    services.AddSingleton<IPageManager, PageManager>();

    services.AddTelerikBlazor();
    services.AddLocalization();
  }
}