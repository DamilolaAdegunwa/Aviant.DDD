namespace Aviant.DDD.Application.Behaviours
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Identity;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public sealed class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ICurrentUserService _currentUserService;

        private readonly IIdentityService _identityIdentityService;

        private readonly ILogger<TRequest> _logger;

        private readonly Stopwatch _timer;

        public PerformanceBehaviour(
            ILogger<TRequest>   logger,
            ICurrentUserService currentUserService,
            IIdentityService    identityIdentityService)
        {
            _timer = new Stopwatch();

            _logger                  = logger;
            _currentUserService      = currentUserService;
            _identityIdentityService = identityIdentityService;
        }

        #region IPipelineBehavior<TRequest,TResponse> Members

        public async Task<TResponse> Handle(
            TRequest                          request,
            CancellationToken                 cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next().ConfigureAwait(false);
            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (500 >= elapsedMilliseconds)
                return response;

            var requestName = typeof(TRequest).Name;
            var userId      = _currentUserService.UserId;
            var username    = string.Empty;

            if (Guid.Empty != userId)
                username = await _identityIdentityService.GetUserNameAsync(userId, cancellationToken)
                   .ConfigureAwait(false);

            _logger.LogWarning(
                "Long Running Request detected: {Name} ({ElapsedMilliseconds} milliseconds), UserId: {@UserId}, Username: {@username}, Request: {@Request}",
                requestName,
                elapsedMilliseconds,
                userId,
                username,
                request);

            return response;
        }

        #endregion
    }
}