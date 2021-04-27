using Burls.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Profiles.Notifications
{
    public class ProfileSelectionRuleCreatedNotification : INotification
    {
        public Profile Profile { get; }
        public SelectionRule SelectionRule { get; }

        public ProfileSelectionRuleCreatedNotification(
            Profile profile,
            SelectionRule selectionRule)
        {
            Profile = profile;
            SelectionRule = selectionRule;
        }
    }
}
