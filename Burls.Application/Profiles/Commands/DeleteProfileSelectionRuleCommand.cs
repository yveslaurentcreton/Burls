using Burls.Application.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Profiles.Commands
{
    public class DeleteProfileSelectionRuleCommand : IRequest, ITransactionalRequest
    {
        public int ProfileId { get; }
        public int SelectionRuleId { get; }

        public DeleteProfileSelectionRuleCommand(int profileId, int selectionRuleId)
        {
            ProfileId = profileId;
            SelectionRuleId = selectionRuleId;
        }
    }
}
