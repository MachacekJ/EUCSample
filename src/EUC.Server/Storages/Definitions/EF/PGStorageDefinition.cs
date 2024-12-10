using EUC.Server.Storages.Contexts.EF;
using EUC.Server.Storages.Definitions.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EUC.Server.Storages.Definitions.EF;

public class PGStorageDefinition : EFStorageDefinition
{
  public override StorageTypeEnum Type => StorageTypeEnum.Postgres;
  public override string DataAnnotationColumnNameKey => "Relational:ColumnName";
  public override string DataAnnotationTableNameKey => "Relational:TableName";
  public override bool IsTransactionEnabled => true;

  public override async Task<bool> DatabaseHasInitUpdate<T>(T dbContext, DbContextOptions options, IMediator mediator, ILogger<DbContextBase> logger)
  {
    var sql = "select count(*) as C from information_schema.tables where table_schema = 'public'";
    var res = await dbContext.Database.SqlQueryRaw<int>(sql).ToListAsync();
    if (res.Count == 0)
      return true;
    return res.First() == 0;
  }

  protected override ObjectId CreatePKObjectId<TEntity, TPK>()
    => throw new Exception($"PK {nameof(ObjectId)} is not allowed for postgres.");

  private class CC
  {
    public long C { get; set; }
  }
}