using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.State
{
    public class ApplicationState : StateBase, IApplicationState
    {
        public string[] StartUpArgs { get; set; }
        public ApplicationMode ApplicationMode { get; set; }
    }

    public enum ApplicationMode
    {
        Select,
        Settings
    }
}
