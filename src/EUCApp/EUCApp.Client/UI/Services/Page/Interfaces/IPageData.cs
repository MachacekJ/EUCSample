﻿using System.Resources;

namespace EUCApp.Client.UI.Services.Page.Interfaces;

public interface IPageData
{
    string Title { get; set; }
    string PageId { get; }
    public string PageUrl { get; }
    
    (ResourceManager Type, string Name)? ResX { get; }
}

