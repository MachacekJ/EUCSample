using EUCApp.Client.UI.Services.App;
using EUCApp.Client.UI.Services.App.Models;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EUCApp.Client.UI.Layouts;

public partial class TelerikLayout(IAppStartConfiguration appSettings) : LayoutComponentBase
{
    private IAppStartConfiguration AppSettings { get; set; } = appSettings;

    [Inject]
    private IAppState AppState { get; set; } = null!;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    [Inject]
    private ILogger<TelerikLayout> Logger { get; set; } = null!;

    [Inject]
    public IMediator Mediator { get; set; } = null!;

    protected override void OnAfterRender(bool firstRender)
    {
        AppState.SetPageState(PageStateEnum.Rendered);
    }
}

