using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.Commands
{
    public class AddPropertyCommand : IRequest
    {
        public object Key { get; }
        public object Value { get; }

        public AddPropertyCommand(object key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
