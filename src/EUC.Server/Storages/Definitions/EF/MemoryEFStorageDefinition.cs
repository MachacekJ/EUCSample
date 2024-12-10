using EUC.Server.Modules.PersistSettingsModule.CQRS.SettingsDbGet;
using EUC.Server.Modules.PersistSettingsModule.Repositories;
using EUC.Server.Storages.Contexts.EF;
using EUC.Server.Storages.Contexts.EF.Models.PK;
using EUC.Server.Storages.Definitions.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EUC.Server.Storages.Definitions.EF;

public class MemoryEFStorageDefinition : EFStorageDefinition
{
  public override StorageTypeEnum Type => StorageTypeEnum.MemoryEF;
  public override string DataAnnotationColumnNameKey => string.Empty;
  public override string DataAnnotationTableNameKey => string.Empty;
  public override bool IsTransactionEnabled => false;

  public override async Task<bool> DatabaseHasInitUpdate<T>(T dbContext, DbContextOptions options, IMediator mediator, ILogger<DbContextBase> logger)
  {
    var isSettingTable = await mediator.Send(new SettingsDbGetQuery(StorageTypeEnum.MemoryEF, $"StorageVersion_{Enum.GetName(typeof(StorageTypeEnum), StorageTypeEnum.MemoryEF)}_{nameof(ISettingsDbModuleRepository)}"));
    return isSettingTable is { IsSuccess: true, ResultValue: null };
  }
  
#pragma warning disable CS8602 // Dereference of a possibly null reference.
  protected override int CreatePKInt<TEntity, TPK>(DbSet<TEntity> dbSet)
    => !dbSet.Any() ? 1 : dbSet.Max(i => (i as PKIntEntity).Id) + 1;

  protected override long CreatePKLong<TEntity, TPK>(DbSet<TEntity> dbSet)
    => !dbSet.Any() ? 1 : dbSet.Max(i => (i as PKLongEntity).Id) + 1;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

  protected override ObjectId CreatePKObjectId<TEntity, TPK>()
    => throw new Exception($"PK {nameof(ObjectId)} is not allowed for memory EF.");
}