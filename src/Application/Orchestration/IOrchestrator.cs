namespace Aviant.DDD.Application.Orchestration
{
    #region

    using System.Threading.Tasks;
    using Commands;
    using Domain.Aggregates;
    using Persistance;
    using Queries;

    #endregion

    public interface IOrchestrator
    {
        Task<RequestResult> SendCommand<T>(ICommand<T> command);

        Task<RequestResult> SendQuery<T>(IQuery<T> query);
    }

    public interface IOrchestrator<TDbContext>
        where TDbContext : IDbContextWrite
    {
        Task<RequestResult> SendCommand<T>(ICommand<T> command);

        Task<RequestResult> SendQuery<T>(IQuery<T> query);
    }

    public interface IOrchestrator<in TAggregate, out TAggregateId>
        where TAggregate : class, IAggregate<TAggregateId>
        where TAggregateId : class, IAggregateId
    {
        Task<RequestResult> SendCommand(ICommand<TAggregate, TAggregateId> command);

        Task<RequestResult> SendQuery<T>(IQuery<T> query);
    }
}