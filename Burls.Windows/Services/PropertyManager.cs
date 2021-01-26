using Burls.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Burls.Windows.Services
{
    public class PropertyManager : IPropertyManager
    {
        public void AddProperty(object key, object value) => Application.Current.Properties.Add(key, value);

        public IEnumerable GetProperties() => Application.Current.Properties;
    }
}
