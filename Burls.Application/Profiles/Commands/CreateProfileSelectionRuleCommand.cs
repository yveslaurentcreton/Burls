using Burls.Application.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Burls.Domain.SelectionRule;

namespace Burls.Application.Profiles.Commands
{
    public class CreateProfileSelectionRuleCommand : IRequest, ITransactionalRequest
    {
        public int ProfileId { get; }
        public SelectionRuleParts SelectionRulePart { get; }
        public SelectionRuleCompareTypes SelectionRuleCompareType { get; }
        public string Value { get; }

        public CreateProfileSelectionRuleCommand(int profileId, SelectionRuleParts selectionRulePart, SelectionRuleCompareTypes selectionRuleCompareType, string value)
        {
            ProfileId = profileId;
            SelectionRulePart = selectionRulePart;
            SelectionRuleCompareType = selectionRuleCompareType;
            Value = value;
        }
    }
}
