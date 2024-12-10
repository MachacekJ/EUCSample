using System.Resources;
using EUCApp.Client.UI.Services.Page.Interfaces;

namespace EUCApp.Client.UI.Services.Page.Implementations;

public class PageData(string pageId, string title, (ResourceManager Type, string Name)? resX)
    : IPageData
{
    public string Title { get; set; } = title;
    public string PageId { get; } = pageId;
    public string PageUrl { get; set; } = pageId;
    public (ResourceManager Type, string Name)? ResX { get; } = resX;

    public PageData(string pageId, string pageUrl, string title, (ResourceManager Type, string Name)? resX) : this(pageId, title, resX)
    {
        PageUrl = pageUrl;
    }
}