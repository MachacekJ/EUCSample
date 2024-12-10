using MediatR;
using Microsoft.Extensions.Logging;

namespace EUC.Server.Storages.Contexts.EF.Scripts;

public abstract class DbVersionScriptsBase
{
    /// <summary>
    /// Version of update.
    /// </summary>
    public abstract Version Version { get; }
    
    /// <summary>
    /// Database script for update database.
    /// </summary>
    public virtual List<string> AllScripts { get; } = new();

    /// <summary>
    /// Call after <see cref="AllScripts" /> executed.
    /// Init data loading etc.
    /// </summary>
    public virtual void AfterScriptRunCode<T>(T dbContext, DbContextOptions options, IMediator mediator, ILogger<DbContextBase> logger) where T : IRepository
    {
      
    }
}