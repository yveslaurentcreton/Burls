using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Burls.Windows.Converters
{
    public class ShortcutToVirtualKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var valueString = value as string;
            
            switch (valueString)
            {
                case "0":
                    return VirtualKey.Number0;
                case "1":
                    return VirtualKey.Number1;
                case "2":
                    return VirtualKey.Number2;
                case "3":
                    return VirtualKey.Number3;
                case "4":
                    return VirtualKey.Number4;
                case "5":
                    return VirtualKey.Number5;
                case "6":
                    return VirtualKey.Number6;
                case "7":
                    return VirtualKey.Number7;
                case "8":
                    return VirtualKey.Number8;
                case "9":
                    return VirtualKey.Number9;
                default:
                    return VirtualKey.None;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
