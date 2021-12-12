using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Services
{
    public class BrowserStateNotificationService : IBrowserStateNotificationService
    {
        public event EventHandler<SelectionRuleCreatedEventArgs> SelectionRuleCreated;
        public event EventHandler<SelectionRuleUpdatedEventArgs> SelectionRuleUpdated;
        public event EventHandler<SelectionRuleDeletedEventArgs> SelectionRuleDeleted;

        public void RaiseSelectionRuleCreated(SelectionRule selectionRule)
        {
            SelectionRuleCreated?.Invoke(this, new SelectionRuleCreatedEventArgs(selectionRule));
        }

        public void RaiseSelectionRuleUpdated(SelectionRule selectionRule)
        {
            SelectionRuleUpdated?.Invoke(this, new SelectionRuleUpdatedEventArgs(selectionRule));
        }

        public void RaiseSelectionRuleDeleted(SelectionRule selectionRule)
        {
            SelectionRuleDeleted?.Invoke(this, new SelectionRuleDeletedEventArgs(selectionRule));
        }
    }

    public class SelectionRuleCreatedEventArgs : EventArgs
    {
        public SelectionRule SelectionRule { get; }

        public SelectionRuleCreatedEventArgs(SelectionRule selectionRule)
        {
            SelectionRule = selectionRule;
        }
    }

    public class SelectionRuleUpdatedEventArgs
    {
        public SelectionRule SelectionRule { get; }

        public SelectionRuleUpdatedEventArgs(SelectionRule selectionRule)
        {
            SelectionRule = selectionRule;
        }
    }

    public class SelectionRuleDeletedEventArgs
    {
        public SelectionRule SelectionRule { get; }

        public SelectionRuleDeletedEventArgs(SelectionRule selectionRule)
        {
            SelectionRule = selectionRule;
        }
    }
}
