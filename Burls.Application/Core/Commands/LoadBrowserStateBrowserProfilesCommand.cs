using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Commands
{
    public class LoadBrowserStateBrowserProfilesCommand : IRequest
    {
        public int? SelectedProfileId { get; }

        public LoadBrowserStateBrowserProfilesCommand(int? selectedProfileId = null)
        {
            SelectedProfileId = selectedProfileId;
        }
    }
}
