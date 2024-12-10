using System.Globalization;
using System.Resources;
using EUCApp.Client.UI.Services.Page.Interfaces;
using EUCApp.Client.UI.Services.Page.Models;

namespace EUCApp.Client.UI.Services.App;

public abstract class AppStartConfigurationBase : IAppStartConfiguration
{
  public abstract string AppName { get; }
  public abstract IEnumerable<MenuHierarchyItem> LeftMenuHierarchy { get; }
  public abstract IEnumerable<IPageData> AllPages { get; }
  public abstract IPageData HomePage { get; }
 

  public void ApplyTranslations()
  {
    foreach (var pageData in AllPages)
    {
      if (pageData.ResX.HasValue)
        pageData.Title = GetLocalizedTitle(pageData.ResX.Value);
    }

    foreach (var menuHierarchyItem in LeftMenuHierarchy)
    {
      HierarchyRecurrence(menuHierarchyItem);
    }
  }

  private static void HierarchyRecurrence(MenuHierarchyItem menuHierarchyItem)
  {
    if (menuHierarchyItem.ResX.HasValue)
      menuHierarchyItem.Title = GetLocalizedTitle(menuHierarchyItem.ResX.Value);

    foreach (var hierarchyItem in menuHierarchyItem.Children)
    {
      HierarchyRecurrence(hierarchyItem);
    }
  }

  private static string GetLocalizedTitle((ResourceManager Type, string Name) resX)
  {
    var lcid = CultureInfo.DefaultThreadCurrentUICulture == null ? 1033 : CultureInfo.DefaultThreadCurrentUICulture.LCID;
    var translation = resX.Type.GetString(resX.Name);
    return string.IsNullOrEmpty(translation) ? 
      $"{lcid}-{resX.Name}-{resX.Name}" 
      : translation;
  }
}