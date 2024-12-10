using EUCApp.Modules.PatientModule.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace EUCApp.Modules.PatientModule.Repositories.Database;

public class PatientDbContext(DbContextOptions<PatientDbContext> options) : DbContext(options)
{
  public DbSet<PatientEntity> Patients { get; set; } = null!;
}