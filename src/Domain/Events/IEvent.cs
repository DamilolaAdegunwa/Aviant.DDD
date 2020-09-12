namespace Aviant.DDD.Domain.Events
{
    #region

    using Aggregates;

    #endregion

    public interface IEvent<out TAggregateId>
        where TAggregateId : IAggregateId
    {
        long AggregateVersion { get; }

        TAggregateId AggregateId { get; }
    }
}