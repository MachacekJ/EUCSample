using EUCApp.Client.CQRS.Results;
using EUCApp.Client.Modules.PatientModule;
using EUCApp.Client.Modules.PatientModule.CQRS.PatientSave;
using EUCApp.Client.Modules.PatientModule.CQRS.Results;
using MediatR;

namespace EUCApp.Modules.PatientModule.CQRS.PatientSave;

/// <summary>
/// Zde by meli probehnout validace DTO, ideálne v <see cref="IPipelineBehavior{TRequest,TResponse}"/>, není implematován.
/// Další napojené <see cref="IPipelineBehavior{TRequest,TResponse}"/> mohou obsahovat logování, duration, opentelemetry, není implementováno
/// Zde taky může publikovat Notifikaci, aby i ostatní moduly, systémy věděli, že jsme něco uložili.
/// </summary>
public class PatientSaveHandler(IPatientModuleRepository patientRepository) : IRequestHandler<PatientSaveCommand, PatientSaveResult>
{
  public async Task<PatientSaveResult> Handle(PatientSaveCommand request, CancellationToken cancellationToken)
  {
    var patientId = await patientRepository.SavePatient(request.Patient);
    return new PatientSaveResult(patientId, true, ResultErrorItem.None);
  }
}