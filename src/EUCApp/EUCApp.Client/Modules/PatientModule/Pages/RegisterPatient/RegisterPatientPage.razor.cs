using EUCApp.Client.Helpers;
using EUCApp.Client.Modules.PatientModule.CQRS.PatientSave;
using EUCApp.Client.Modules.PatientModule.Models;
using EUCApp.Client.Modules.PatientModule.ResX;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Telerik.Blazor.Components;

namespace EUCApp.Client.Modules.PatientModule.Pages.RegisterPatient;

public partial class RegisterPatientPage(IStringLocalizer<ResXPatient> localizer, IMediator mediator)
{
  private bool ValidSubmit { get; set; } = false;
  private EditContext PatientEditContext { get; set; }
  TelerikNotification NotificationComponent { get; set; }
  private RegisterPatientViewModel Model { get; set; } = new();
  private RegisterPatientViewModelValidator Validator { get; set; } = new();
  private List<GenderItem> Genders { get; } = new();

  private IEnumerable<NationalityItem> Nationalities { get; set; } = new List<NationalityItem>()
  {
    new("cs", localizer["nationalityCs"]),
    new("de", localizer["nationalityDe"]),
    new("pl", localizer["nationalityPl"]),
  };

  protected override void OnInitialized()
  {
    foreach (GenderEnum item in Enum.GetValues(typeof(GenderEnum)))
    {
      Genders.Add(new GenderItem { Id = item, Name = localizer[$"Gender{Enum.GetName(item)}"] });
    }

    PatientEditContext = new EditContext(Model);
    base.OnInitialized();
  }

  private async Task HandleValidSubmit()
  {
    ValidSubmit = true;

    var result = await mediator.Send(new PatientSaveCommand(Model.ToDtoModel()));
    Model.Id = result.PatientId;

    await Task.Delay(1000);

    ValidSubmit = false;
    StateHasChanged();
    ShowNotifications();
  }

  private void HandleInvalidSubmit()
  {
    ValidSubmit = false;
  }

  private void DateNumberChange()
  {
    if (!Model.IsBirthNumber)
      return;

    var birthNumberHelper = new BirthNumberHelper(Model.BirthNumber);
    if (birthNumberHelper is not { IsValid: true, BirthDate: not null, IsMale: not null })
      return;

    Model.BirthDate = birthNumberHelper.BirthDate.Value;
    Model.Gender = birthNumberHelper.IsMale.Value ? GenderEnum.Male : GenderEnum.Female;
  }

  private void ShowNotifications()
  {
    NotificationComponent.Show(new NotificationModel
    {
      Text = @localizer[ResXPatient.Saved],
      ThemeColor = "success",
      CloseAfter = 0
    });
  }
}