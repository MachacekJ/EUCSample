using EUCApp.Client.UI.Services.Page.Models;

namespace EUCApp.Client.UI.Services.Page.Interfaces;

public interface IPageManager
{
    Task<IEnumerable<BreadcrumbItem>> GetBreadcrumbsForPageAsync(IPageData page);
}