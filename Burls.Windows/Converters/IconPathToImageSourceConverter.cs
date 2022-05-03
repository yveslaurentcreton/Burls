using Burls.Windows.Core;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace Burls.Windows.Converters
{
    public class IconPathToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string iconPath)
            {
                var iconFileInfo = new FileInfo(iconPath);

                switch (iconFileInfo.Extension)
                {
                    case ".ico":
                        return XamlBindingHelper.ConvertValue(typeof(ImageSource), iconPath);
                    case ".exe":
                        var storageFile = StorageFile.GetFileFromPathAsync(iconPath).GetAwaiter().GetResult();
                        var appIcon = storageFile.GetThumbnailAsync(ThumbnailMode.SingleItem).GetAwaiter().GetResult();
                        var imgSource = new BitmapImage();
                        imgSource.SetSource(appIcon);
                        
                        return XamlBindingHelper.ConvertValue(typeof(ImageSource), imgSource);
                    default:
                        break;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
