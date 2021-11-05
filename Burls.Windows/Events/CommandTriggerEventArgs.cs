using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Events
{
    public class CommandTriggerEventArgs
    {
        public string CommandParameter { get; }

        public CommandTriggerEventArgs(string commandParameter)
        {
            CommandParameter = commandParameter;
        }
    }
}
