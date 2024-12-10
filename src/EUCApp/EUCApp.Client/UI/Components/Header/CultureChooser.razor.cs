using System.Globalization;
using EUCApp.Client.Configuration;
using EUCApp.Client.UI.Services.App.Models;

namespace EUCApp.Client.UI.Components.Header;

public partial class CultureChooser : BlazorComponentBase
{
    private LanguageItem _value = SupportedLanguage.AllSupportedLanguages.First();

    private static CultureInfo Culture => CultureInfo.CurrentUICulture;

    protected override void OnInitialized()
    {
        var result = SupportedLanguage.AllSupportedLanguages.FirstOrDefault(a => a.Id == Culture.Name);
        if (result == null)
        {
            _value = SupportedLanguage.AllSupportedLanguages.First();
            return;
        }
        _value = result;
    }

    private void ShowContextMenu()
    {
        AppState.ShowRightMenu(RightMenuTypeEnum.Language);
    }
}