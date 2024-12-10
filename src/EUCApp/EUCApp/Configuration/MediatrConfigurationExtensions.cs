using FluentValidation;

namespace EUCApp.Configuration;

public static class MediatrConfigurationExtensions
{
  /// <summary>
  /// Registace madiatr, pro Server cast a validaci.
  /// </summary>
  /// <param name="services"></param>
  public static void AddEUCMediatrAndValidation(this IServiceCollection services)
  {
    services.AddMediatR((c) =>
    {
      c.RegisterServicesFromAssemblyContaining(typeof(Program));
    });

    // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
    // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationPipelineBehavior<,>));
    services.AddValidatorsFromAssembly(typeof(Client._Imports).Assembly);
    services.AddValidatorsFromAssembly(typeof(Program).Assembly);
  }
}