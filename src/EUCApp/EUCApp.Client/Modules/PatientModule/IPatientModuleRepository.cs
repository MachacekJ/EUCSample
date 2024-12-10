using System.Reflection.Metadata;
using EUCApp.Client.Modules.PatientModule.CQRS.Models;

namespace EUCApp.Client.Modules.PatientModule;

public interface IPatientModuleRepository
{
  Task<int> SavePatient(PatientDto patient);
  Task<IEnumerable<PatientDto>> GetAllPatients();
  Task<PatientDto?> GetPatient(int patientId);
}