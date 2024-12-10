using EUCApp.Client.UI.Services.App;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace EUCApp.Client.UI.Components;

public abstract class BlazorComponentBase : ComponentBase //(IMediator mediator, IAppState appState, ILogger<JMComponentBase> log
{
  [Inject]
  public IMediator Mediator { get; set; } = null!;

  [Inject]
  public IAppState AppState { get; set; } = null!;

  [Inject]
  public ILogger<BlazorComponentBase> Log { get; set; } = null!;
}