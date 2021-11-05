using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsUrl(this string value)
        {
            return Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var uri);
        }
    }
}
