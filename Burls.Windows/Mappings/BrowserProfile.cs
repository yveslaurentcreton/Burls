using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Windows.Mappings
{
    public class BrowserProfile : AutoMapper.Profile
    {
        public BrowserProfile()
        {
            CreateMap<InstalledBrowser, Browser>()
                .ForMember(b => b.Profiles, opt => opt.PreCondition((i, b, c) => b.Id == 0));
        }
    }
}
