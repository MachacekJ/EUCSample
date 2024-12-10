using EUCApp.Client.UI.Services.Page.Interfaces;
using EUCApp.Client.UI.Services.Page.Models;

namespace EUCApp.Client.UI.Services.App;

public interface IAppStartConfiguration
{
    string AppName { get; }
    IEnumerable<MenuHierarchyItem> LeftMenuHierarchy { get; }
    IEnumerable<IPageData> AllPages { get; }
    IPageData HomePage { get; }
    void ApplyTranslations();
}