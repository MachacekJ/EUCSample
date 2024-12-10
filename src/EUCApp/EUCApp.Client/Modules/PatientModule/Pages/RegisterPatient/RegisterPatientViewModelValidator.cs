using EUCApp.Client.Modules.PatientModule.Components.BirthNumber;
using EUCApp.Client.Modules.PatientModule.ResX;
using FluentValidation;

namespace EUCApp.Client.Modules.PatientModule.Pages.RegisterPatient;

/// <summary>
/// Validuje hodnoty na formálři.
/// </summary>
public class RegisterPatientViewModelValidator : AbstractValidator<RegisterPatientViewModel>
{
  public RegisterPatientViewModelValidator()
  {
    RuleFor(x => x.FirstName).NotEmpty().WithName(ResXPatient.FirstName);
    RuleFor(x => x.LastName).NotEmpty().WithName(ResXPatient.LastName);
    // podmíněná validace
    When(x => x.IsBirthNumber, () =>
    {
      RuleFor(x => x.BirthNumber).HasValidBirthNumber();
      RuleFor(x => x.BirthNumber).NotEmpty().WithName(ResXPatient.BirthNumber);
    });
  
    RuleFor(x => x.BirthDate).NotEmpty().WithName(ResXPatient.BirthDate);;
    RuleFor(x => x.Gender).NotNull().WithName(ResXPatient.Gender);;
    RuleFor(x => x.Email).NotNull().EmailAddress().WithName(ResXPatient.Email);;
    RuleFor(x => x.Nationality).NotNull().WithName(ResXPatient.Nationality);;
    RuleFor(x => x.GdprApproval).Must(x=>x.HasValue && x.Value).WithMessage(ResXPatient.GDRP_RequiredMessage);
  }
}