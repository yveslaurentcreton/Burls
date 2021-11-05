using Burls.Application.Browsers.Services;
using Burls.Application.Browsers.State;
using Burls.Application.Core.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burls.Application.Core.Handlers
{
    public class LoadBrowserStateBrowserProfilesCommandHandler : IRequestHandler<LoadBrowserStateBrowserProfilesCommand>
    {
        private readonly IBrowserState _browserState;
        private readonly IBrowserService _browserService;

        public LoadBrowserStateBrowserProfilesCommandHandler(
            IBrowserState browserState,
            IBrowserService browserService)
        {
            _browserState = browserState;
            _browserService = browserService;
        }

        public async Task<Unit> Handle(LoadBrowserStateBrowserProfilesCommand request, CancellationToken cancellationToken)
        {
            _browserState.BrowserProfiles = await _browserService.GetBrowserProfilesAsync();
            _browserState.SelectedBrowserProfile = _browserState.BrowserProfiles?.FirstOrDefault(x => x.Profile.Id == request.SelectedProfileId);

            return Unit.Value;
        }
    }
}
