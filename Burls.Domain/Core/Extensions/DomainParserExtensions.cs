using Nager.PublicSuffix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Domain.Core.Extensions
{
    public static class DomainParserExtensions
    {
        public static bool CanParse(this DomainParser domainParser, string domain)
        {
            try
            {
                domainParser.Parse(domain);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
