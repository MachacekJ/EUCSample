using EUCApp.Client.Modules.PatientModule.CQRS.PatientSave;
using EUCApp.Client.Modules.PatientModule.Models;

namespace EUCApp.Client.Modules.PatientModule.CQRS.Models;

/// <summary>
/// Take a look at <see cref="PatientSaveValidator"/>.
/// </summary>
public class PatientDto
{
  public string FirstName { get; set; } = string.Empty;
  
  public string LastName { get; set; } = string.Empty;
  
  public string Email { get; set; } = string.Empty;
  
  public GenderEnum Gender { get; set; } = GenderEnum.Male;

  public string? BirthNumber { get; set; }
  
  public DateTime BirthDate { get; set; } = DateTime.MinValue;
  
  public string Nationality { get; set; } = string.Empty;
  
  public bool? GdprApproval { get; set; } = null;
}