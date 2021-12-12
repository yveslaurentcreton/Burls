using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Burls.Domain.SelectionRule;

namespace Burls.Windows.Converters
{
    public class SelectionRulePartsToStringLowerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((SelectionRuleParts)value)
            {
                case SelectionRuleParts.Url:
                    return "url";
                case SelectionRuleParts.Hostname:
                    return "hostname";
                case SelectionRuleParts.Domain:
                    return "domain";
                case SelectionRuleParts.Subdomain:
                    return "subdomain";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
