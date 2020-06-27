using Burls.Windows.Models;
using System.Collections.Generic;

namespace Burls.Windows.Services
{
    public interface IProfileService
    {
        IReadOnlyList<Profile> GetProfiles(string name);
    }
}