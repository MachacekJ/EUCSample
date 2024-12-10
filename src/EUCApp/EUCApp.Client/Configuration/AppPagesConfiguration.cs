using EUCApp.Client.ResX;
using EUCApp.Client.UI.Services.App;
using EUCApp.Client.UI.Services.Page.Implementations;
using EUCApp.Client.UI.Services.Page.Interfaces;
using EUCApp.Client.UI.Services.Page.Models;
using Telerik.SvgIcons;

namespace EUCApp.Client.Configuration;

public class AppPagesConfiguration : AppStartConfigurationBase
{
  public override string AppName => "UI test";
  public override IPageData HomePage => AppPageList.Home;

  public override IEnumerable<IPageData> AllPages { get; } = new List<IPageData>
  {
    AppPageList.NotFound,
    AppPageList.Home,
    AppPageList.Patient,
    AppPageList.PatientExport,
    AppPageList.About,
  };

  public override IEnumerable<MenuHierarchyItem> LeftMenuHierarchy { get; } = new List<MenuHierarchyItem>
  {
    new(AppPageList.Home, SvgIcon.Home),
    new(AppPageList.Patient, SvgIcon.Accessibility),
    new(AppPageList.PatientExport, SvgIcon.Export),
    new(AppPageList.About, SvgIcon.InfoCircle)
  };
}

public static class AppPageList
{
  public static readonly IPageData NotFound = PageDataBuilder.BuildNotFoundPage();
  public static readonly IPageData Home = new PageDataBuilder("home", "Home").SetUrl("/").Build();
  public static readonly IPageData Patient = new PageDataBuilder("/patient", "Patient").SetResX(ResXMenu.ResourceManager, nameof(ResXMenu.Patient)).Build();
  public static readonly IPageData PatientExport = new PageDataBuilder("/export", "Patient export").SetResX(ResXMenu.ResourceManager, nameof(ResXMenu.PatientExport)).Build();
  public static readonly IPageData About = new PageDataBuilder("/about", "About").SetResX(ResXMenu.ResourceManager, nameof(ResXMenu.About)).Build();
}