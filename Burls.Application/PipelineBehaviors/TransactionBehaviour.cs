using Burls.Application.Core;
using Burls.Application.Core.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Application.PipelineBehaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ITransactionalRequest
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehaviour(
            IUnitOfWork unitOfWork,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default;

            try
            {
                await _unitOfWork.RetryOnExceptionAsync(async (cancellationToken) =>
                {
                    _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

                    await _unitOfWork.BeginTransactionAsync();

                    response = await next();

                    await _unitOfWork.CommitTransactionAsync();

                    _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");
                });

                return response;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");

                _unitOfWork.RollbackTransaction();

                _logger.LogError(e.Message, e.StackTrace);

                throw;
            }
        }
    }
}
