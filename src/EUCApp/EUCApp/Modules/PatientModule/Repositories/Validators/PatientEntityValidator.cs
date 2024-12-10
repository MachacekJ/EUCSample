using EUCApp.Modules.PatientModule.Repositories.Models;
using FluentValidation;

namespace EUCApp.Modules.PatientModule.Repositories.Validators;

/// <summary>
/// Zde bychom validovali např. jestli už existuje email adresa ci rodne cislo,
/// popr. jestli je kod narodnosti v ciselniku atd.
/// </summary>
public class PatientEntityValidator : AbstractValidator<PatientEntity>
{
  public PatientEntityValidator()
  {
    RuleFor(x => x.FirstName).NotEmpty();
    //TODO
  }
}