using EUCApp.Client.UI.Components.SvgIcons;
using EUCApp.Client.UI.Services.App.Models;

namespace EUCApp.Client.Configuration;

public static class SupportedLanguage
{
    public static IEnumerable<LanguageItem> AllSupportedLanguages { get; } = new[] {
        new LanguageItem (1033,"en-US", "English", SvgNationalFlagIcons.EnUs ),
        new LanguageItem ( 1029, "cs-CZ", "Čeština", SvgNationalFlagIcons.CsCz ) };
}