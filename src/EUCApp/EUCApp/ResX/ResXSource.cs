using System.Resources;
using EUC.Core.Localizer;

namespace JMCore.Server.ResX;

public class ResXSource(string contextId, ResourceManager resourceManager, LocalizationScopeEnum scope)
{
    public string ContextId { get; } = contextId;
    public ResourceManager ResourceManager { get; } = resourceManager;
    public LocalizationScopeEnum Scope { get; } = scope;
}