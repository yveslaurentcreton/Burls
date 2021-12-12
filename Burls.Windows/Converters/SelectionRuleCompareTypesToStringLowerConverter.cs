using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Burls.Domain.SelectionRule;

namespace Burls.Windows.Converters
{
    public class SelectionRuleCompareTypesToStringLowerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((SelectionRuleCompareTypes)value)
            {
                case SelectionRuleCompareTypes.Equals:
                    return "equals";
                case SelectionRuleCompareTypes.Contains:
                    return "contains";
                case SelectionRuleCompareTypes.StartsWith:
                    return "starts with";
                case SelectionRuleCompareTypes.EndsWith:
                    return "ends with";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
