using Burls.Domain;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Converters
{
    public class SelectionRuleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var selectionRule = value as SelectionRule;

            return $"{selectionRule.SelectionRulePart} {selectionRule.SelectionRuleCompareType.ToString().ToLower()} {selectionRule.Value.ToLower()}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
