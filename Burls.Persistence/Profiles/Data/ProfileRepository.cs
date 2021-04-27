using Burls.Application.Profiles.Data;
using Burls.Domain;
using Burls.Persistence.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Persistence.Profiles.Data
{
    public class ProfileRepository : RepositoryBase<Profile, BurlsDbContext>, IProfileRepository
    {
        public ProfileRepository(BurlsDbContext context)
            : base(context)
        {
        }
    }
}
