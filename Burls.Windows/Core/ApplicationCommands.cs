using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Core
{
    public static class ApplicationCommands
    {
        public static CompositeCommand ShowFlyoutCommand = new CompositeCommand();
        public static CompositeCommand CloseFlyoutCommand = new CompositeCommand();
    }

    public interface IApplicationCommands
    {
        CompositeCommand ShowFlyoutCommand { get; }
        CompositeCommand CloseFlyoutCommand { get; }
    }

    public class ApplicationCommandsProxy : IApplicationCommands
    {
        public CompositeCommand ShowFlyoutCommand
        {
            get { return ApplicationCommands.ShowFlyoutCommand; }
        }

        public CompositeCommand CloseFlyoutCommand
        {
            get { return ApplicationCommands.CloseFlyoutCommand; }
        }
    }
}
