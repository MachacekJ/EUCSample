using System.ComponentModel.DataAnnotations;
using EUCApp.Client.Modules.PatientModule.CQRS.Models;
using EUCApp.Client.Modules.PatientModule.Models;
using EUCApp.Client.Modules.PatientModule.ResX;
using Mapster;

namespace EUCApp.Client.Modules.PatientModule.Pages.RegisterPatient;

/// <summary>
/// Hodnoty pro controly formuláře.
/// Zpravidla se mění, když se změní hodnota ve formuláři.
/// Na tento model je napojen <see cref="RegisterPatientViewModelValidator"/>, který hodnoty validuje
/// a pokud nejsou validní, na formuláři se objeví validační hláška.
/// </summary>
public class RegisterPatientViewModel
{
  public int Id { get; set; }
  
  [Display(ResourceType = typeof(ResXPatient), Name = nameof(ResXPatient.FirstName))]
  public string FirstName { get; set; } = string.Empty;
  
  [Display(ResourceType = typeof(ResXPatient), Name = nameof(ResXPatient.LastName))]
  public string LastName { get; set; } = string.Empty;
  
  [Display(ResourceType = typeof(ResXPatient), Name = nameof(ResXPatient.Email))]
  public string Email { get; set; } = string.Empty;
  
  [Display(ResourceType = typeof(ResXPatient), Name = nameof(ResXPatient.Gender))]
  public GenderEnum? Gender { get; set; } = GenderEnum.Male;

  [Display(ResourceType = typeof(ResXPatient), Name = nameof(ResXPatient.BirthNumber))]
  public string? BirthNumber { get; set; }
  
  [Display(ResourceType = typeof(ResXPatient), Name = nameof(ResXPatient.BirthDate))]
  public DateTime? BirthDate { get; set; }
  
  [Display(ResourceType = typeof(ResXPatient), Name = nameof(ResXPatient.Nationality))]
  public string Nationality { get; set; } = "cs";
  
  [Display(ResourceType = typeof(ResXPatient), Name = nameof(ResXPatient.Gdpr))]
  public bool? GdprApproval { get; set; }

  public bool IsBirthNumber { get; set; } = true;
}

public static class RegisterPatientPageModelExtensions
{
  public static RegisterPatientViewModel Create(this PatientDto model)
  {
    return model.Adapt<RegisterPatientViewModel>();
  }

  public static PatientDto ToDtoModel(this RegisterPatientViewModel model)
  {
    return model.Adapt<PatientDto>();
  }

}