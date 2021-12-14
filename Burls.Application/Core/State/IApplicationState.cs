using Burls.Domain;

namespace Burls.Application.Core.State
{
    public interface IApplicationState : IState
    {
        string[] StartUpArgs { get; set; }
        ApplicationMode ApplicationMode { get; set; }
        Settings Settings { get; }
    }
}