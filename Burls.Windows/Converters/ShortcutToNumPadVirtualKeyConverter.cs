using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Burls.Windows.Converters
{
    public class ShortcutToNumPadVirtualKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var valueString = value as string;
            
            switch (valueString)
            {
                case "0":
                    return VirtualKey.NumberPad0;
                case "1":
                    return VirtualKey.NumberPad1;
                case "2":
                    return VirtualKey.NumberPad2;
                case "3":
                    return VirtualKey.NumberPad3;
                case "4":
                    return VirtualKey.NumberPad4;
                case "5":
                    return VirtualKey.NumberPad5;
                case "6":
                    return VirtualKey.NumberPad6;
                case "7":
                    return VirtualKey.NumberPad7;
                case "8":
                    return VirtualKey.NumberPad8;
                case "9":
                    return VirtualKey.NumberPad9;
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
