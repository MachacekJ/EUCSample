using EUC.Server.Storages.Models.EntityEvent;
using MediatR;

namespace EUC.Server.Storages.CQRS.Notifications;

/// <summary>
/// This event is fired when some operation for storage was executed.
/// </summary>
public class EntityEventNotification(EntityEventItem entityEventItem) : INotification
{
  public EntityEventItem EntityEvent => entityEventItem;
}
