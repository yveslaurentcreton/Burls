using Burls.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.EquivalencyExpression;
using System.Collections.ObjectModel;

namespace Burls.Windows.Mappings
{
    public class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile()
        {
            CreateMap<InstalledProfile, Profile>()
                .EqualityComparison((source, destination) => source.Name.Equals(destination.Name))
                .ForMember(p => p.SelectionRules, opt => opt.UseDestinationValue());
        }
    }
}
