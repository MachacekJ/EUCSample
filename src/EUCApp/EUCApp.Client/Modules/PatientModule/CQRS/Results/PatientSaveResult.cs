using EUCApp.Client.CQRS.Results;

namespace EUCApp.Client.Modules.PatientModule.CQRS.Results;

public class PatientSaveResult : Result
{
  public int PatientId { get; }

  public PatientSaveResult(int patientId, bool isSuccess, ResultErrorItem resultErrorItem) : base(isSuccess, resultErrorItem)
  {
    PatientId = patientId;
  }
}