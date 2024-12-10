using EUCApp.Client.Modules.PatientModule;
using EUCApp.Client.Modules.PatientModule.CQRS.Models;
using EUCApp.Modules.PatientModule.Repositories.Database;
using EUCApp.Modules.PatientModule.Repositories.Models;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EUCApp.Modules.PatientModule.Repositories;

/// <summary>
/// Implementace <see cref="IPatientModuleRepository"/> -> ukladání dat do DB přes EF.
/// </summary>
/// <param name="validator"></param>
/// <param name="patientDbContext"></param>
public class PatientModuleRepository(IValidator<PatientEntity> validator, PatientDbContext patientDbContext) : IPatientModuleRepository
{
  public async Task<int> SavePatient(PatientDto patient)
  {
    var patientEntity = patient.Adapt<PatientEntity>();

    // business validace - konzistence dat.
    var businessValidatorResult = await validator.ValidateAsync(patientEntity);
    if (!businessValidatorResult.IsValid)
    {
      throw new ValidationException(businessValidatorResult.Errors);
    }

    patientDbContext.Patients.Add(patientEntity);

    await patientDbContext.SaveChangesAsync();
    return patientEntity.Id;
  }

  public async Task<IEnumerable<PatientDto>> GetAllPatients()
  {
    var dbResult = await patientDbContext.Patients.ToListAsync();
    return dbResult.Select(res => res.Adapt<PatientDto>()).ToList();
  }

  public async Task<PatientDto?> GetPatient(int patientId)
  {
    var dbResult = await patientDbContext.Patients.FindAsync(patientId);
    return dbResult?.Adapt<PatientDto>();
  }
}