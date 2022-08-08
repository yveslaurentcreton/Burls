using Burls.Domain;

namespace Burls.Windows.ViewModels.Models.Messages
{
    public class SelectionRuleRemoved
    {
        public SelectionRule SelectionRule { get; }

        public SelectionRuleRemoved(SelectionRule selectionRule)
        {
            SelectionRule = selectionRule;
        }
    }
}
