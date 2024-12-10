using EUCApp.Client.Modules.PatientModule.CQRS.Results;
using MediatR;

namespace EUCApp.Client.Modules.PatientModule.CQRS.PatientSave;

public class PatientSaveWebHandler() : IRequestHandler<PatientSaveCommand, PatientSaveResult>
{
  public Task<PatientSaveResult> Handle(PatientSaveCommand request, CancellationToken cancellationToken)
  {
    // tady se provolava web api, kdyz jsme v rezimu WebAssembly
    throw new NotImplementedException();
  }
}