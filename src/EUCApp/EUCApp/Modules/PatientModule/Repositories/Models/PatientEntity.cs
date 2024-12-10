using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EUCApp.Client.Modules.PatientModule.Models;
using EUCApp.Modules.PatientModule.Repositories.Validators;

namespace EUCApp.Modules.PatientModule.Repositories.Models;

/// <summary>
/// Pouzivame fluent validation, nejsou tu atributy napr. <see cref="RequiredAttribute"/>
/// Fluent validation <see cref="PatientEntityValidator"/>
/// </summary>
[Table("Patients")]
public class PatientEntity
{
  [Key]
  public int Id { get; set; }

  [MaxLength(50)]
  public required string FirstName { get; set; }

  [MaxLength(50)]
  public required string LastName { get; set; }

  [MaxLength(255)]
  public required string Email { get; set; }

  [MaxLength(10)]
  public string? BirthNumber { get; set; }

  public GenderEnum Gender { get; set; }
  public DateTime BirthDate { get; set; }

  [MaxLength(8)]
  public string? Nationality { get; set; }

  public bool? GdprApproval { get; set; }
}