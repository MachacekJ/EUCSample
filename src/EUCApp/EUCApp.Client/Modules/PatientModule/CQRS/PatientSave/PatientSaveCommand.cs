using EUCApp.Client.Modules.PatientModule.CQRS.Models;
using EUCApp.Client.Modules.PatientModule.CQRS.Results;
using MediatR;

namespace EUCApp.Client.Modules.PatientModule.CQRS.PatientSave;

/// <summary>
/// Vyvola <see cref="IRequestHandler{TRequest,TResponse}"/>, dle DI se najde implementace bud Server nebo WebAssembly.
/// </summary>
/// <param name="Patient"></param>
public record PatientSaveCommand(PatientDto Patient) : IRequest<PatientSaveResult>;

