using EUCApp.Client.Modules.PatientModule.CQRS.Models;
using EUCApp.Client.Modules.PatientModule.ResX;
using FluentValidation;
using MediatR;

namespace EUCApp.Client.Modules.PatientModule.CQRS.PatientSave;

/// <summary>
/// Validace input dat pro <see cref="IRequestHandler{TRequest}"/>.
/// Většinou zpracovává vrstva <see cref="IPipelineBehavior{TRequest,TResponse}"/>
/// </summary>
public class PatientSaveValidator : AbstractValidator<PatientDto>
{
  public PatientSaveValidator()
  {
    RuleFor(x => x.FirstName).NotEmpty().WithName(ResXPatient.FirstName);
    RuleFor(x => x.LastName).NotEmpty().WithName(ResXPatient.LastName);
    RuleFor(x => x.BirthDate).NotEmpty();
    //TODO
  }
}