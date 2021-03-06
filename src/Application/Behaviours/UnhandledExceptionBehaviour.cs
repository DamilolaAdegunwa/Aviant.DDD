namespace Aviant.DDD.Application.Behaviours
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public sealed class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger) => _logger = logger;

        #region IPipelineBehavior<TRequest,TResponse> Members

        public async Task<TResponse> Handle(
            TRequest                          request,
            CancellationToken                 cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            Console.WriteLine("************");
            Console.WriteLine(typeof(TRequest).Name);
            Console.WriteLine(request.ToString());
            Console.WriteLine("************");

            try
            {
                return await next().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(
                    ex,
                    "Unhandled Exception for Request {Name} {@Request}",
                    requestName,
                    request);

                throw;
            }
        }

        #endregion
    }
}