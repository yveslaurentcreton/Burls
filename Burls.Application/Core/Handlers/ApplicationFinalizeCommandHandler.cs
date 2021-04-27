using Burls.Application.Core.Commands;
using Burls.Application.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Application.Core.Handlers
{
    public class ApplicationFinalizeCommandHandler : IRequestHandler<ApplicationFinalizeCommand>
    {
        private readonly IPersistAndRestoreService _persistAndRestoreService;

        public ApplicationFinalizeCommandHandler(IPersistAndRestoreService persistAndRestoreService)
        {
            _persistAndRestoreService = persistAndRestoreService;
        }

        public Task<Unit> Handle(ApplicationFinalizeCommand request, CancellationToken cancellationToken)
        {
            _persistAndRestoreService.PersistData();

            return Unit.Task;
        }
    }
}
