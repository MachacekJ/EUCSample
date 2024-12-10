using MediatR;

namespace EUC.Server.Modules.PersistSettingsModule.CQRS;

public class SettingsDbModuleRequest<TResponse> : IRequest<TResponse>
  where TResponse : Result;