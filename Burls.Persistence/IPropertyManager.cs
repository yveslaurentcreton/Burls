using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence
{
    public interface IPropertyManager
    {
        IEnumerable GetProperties();
        void AddProperty(object key, object? value);
    }
}
