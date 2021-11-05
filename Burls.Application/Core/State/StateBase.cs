using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Application.Core.State
{
    public class StateBase : IState
    {
        public event EventHandler StateChanged;

        public void RaiseStateChanged()
        {
            StateChanged?.Invoke(this, new EventArgs());
        }
    }
}
