using EUCApp.Client.Modules.PatientModule;
using EUCApp.Modules.PatientModule.Repositories;
using EUCApp.Modules.PatientModule.Repositories.Database;
using Microsoft.EntityFrameworkCore;

namespace EUCApp.Modules.PatientModule.Configuration;

public static class PatientModuleStartupExtensions
{
  /// <summary>
  /// Registrace EF a repository do IoC.
  /// </summary>
  public static void AddPatientModule(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<PatientDbContext>(b => b.UseSqlServer(
      configuration.GetConnectionString("DefaultConnection")));
    services.AddScoped<IPatientModuleRepository, PatientModuleRepository>();
  }
  
  /// <summary>
  /// Zaregistrujeme ednpointy pro pracienty. Minimal API
  /// </summary>
  public static void MapPatientModuleEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("api/patient");
    group.MapGet("/", async (IPatientModuleRepository patientModuleRepository) =>
    {
      var patients = await patientModuleRepository.GetAllPatients();
      return Results.Ok(patients);
    });
    group.MapGet("/{id:int}", async (IPatientModuleRepository patientModuleRepository, int id) =>
    {
      var patient = await patientModuleRepository.GetPatient(id);
      return Results.Ok(patient);
    });
  }
}