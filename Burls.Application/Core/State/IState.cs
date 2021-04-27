using System;

namespace Burls.Application.Core.State
{
    public interface IState
    {
        event EventHandler StateChanged;

        void RaiseStateChanged();
    }
}