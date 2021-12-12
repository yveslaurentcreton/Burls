using Burls.Domain;
using System;

namespace Burls.Windows.Services
{
    public interface IBrowserStateNotificationService
    {
        event EventHandler<SelectionRuleCreatedEventArgs> SelectionRuleCreated;
        event EventHandler<SelectionRuleUpdatedEventArgs> SelectionRuleUpdated;
        event EventHandler<SelectionRuleDeletedEventArgs> SelectionRuleDeleted;

        void RaiseSelectionRuleCreated(SelectionRule selectionRule);
        void RaiseSelectionRuleUpdated(SelectionRule selectionRule);
        void RaiseSelectionRuleDeleted(SelectionRule selectionRule);
    }
}