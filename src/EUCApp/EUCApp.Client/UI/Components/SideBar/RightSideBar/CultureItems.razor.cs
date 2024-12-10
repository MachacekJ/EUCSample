using System.Globalization;
using EUCApp.Client.Configuration;
using EUCApp.Client.UI.Services.App.Models;
using EUCApp.Client.UI.Services.JS;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Telerik.Blazor.Components;
using Telerik.Blazor.Components.PanelBar.Models;

namespace EUCApp.Client.UI.Components.SideBar.RightSideBar;

public partial class CultureItems(IJSRuntime? jsRuntime, NavigationManager? navigationManager)
{
  private List<PanelBarItem> _rootItems = new();
  private IJSRuntime JsRuntime { get; set; } = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
  private NavigationManager NavigationManager { get; set; } = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
  
  protected override void OnInitialized()
  {
    foreach (var item in SupportedLanguage.AllSupportedLanguages)
    {
      _rootItems.Add(new PanelBarItem
      {
        Id = item.LCID,
        Text = item.Text,
        Icon = item.Icon,
        DataItem = item
      });
    }
  }

  private async Task LanguageChange(PanelBarItemClickEventArgs panelBarItem)
  {
    var lang = (panelBarItem.Item as PanelBarItem)?.DataItem as LanguageItem;
    if (lang == null || CultureInfo.CurrentUICulture.LCID == lang.LCID)
      return;

    Log.LogInformation("Change culture {culture}", lang.LCID);

    // await Mediator.Send(new WriteAnalyticsCommand(new AnalyticsData
    //     { AnalyticsTypeEnum = AnalyticsTypeEnum.UI, 
    //         Name = AnalyticsName.CultureChange, 
    //         Value = lang.LCID.ToString() }));
    await JsRuntime.SetAspNetCoreCultureCookie(lang.Id);
    NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
  }
}