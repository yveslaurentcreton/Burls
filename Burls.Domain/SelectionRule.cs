using Burls.Domain.Core.Extensions;
using Nager.PublicSuffix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burls.Domain
{
    public class SelectionRule
    {
        public enum SelectionRuleParts
        {
            [Description("Url")]
            Url,
            [Description("Hostname")]
            Hostname,
            [Description("Domain")]
            Domain,
            [Description("Subdomain")]
            Subdomain
        }

        public enum SelectionRuleCompareTypes
        {
            [Description("Equals")]
            Equals,
            [Description("Contains")]
            Contains,
            [Description("Starts with")]
            StartsWith,
            [Description("Ends with")]
            EndsWith
        }

        public static string GetPartFromUrl(SelectionRuleParts selectionRulePart, string url)
        {
            switch (selectionRulePart)
            {
                case SelectionRuleParts.Url:
                    return url;
                case SelectionRuleParts.Hostname:
                    return GetDomainInfo(url)?.Hostname;
                case SelectionRuleParts.Domain:
                    return GetDomainInfo(url)?.RegistrableDomain;
                case SelectionRuleParts.Subdomain:
                    return GetDomainInfo(url)?.SubDomain;
            }

            return null;
        }

        public static DomainInfo GetDomainInfo(string url)
        {
            var domainParser = new DomainParser(new WebTldRuleProvider());
            var adjustedUrl = url.Replace("http://", string.Empty); // TODO: Look for an alternative or add fix to Nager.PublicSuffix so that http:// is also supported
            var domainInfo = domainParser.CanParse(adjustedUrl) ? domainParser.Parse(adjustedUrl) : null;

            return domainInfo;
        }

        public int Id { get; protected set; }
        public int ProfileId { get; protected set; }
        public SelectionRuleParts SelectionRulePart { get; set; }
        public SelectionRuleCompareTypes SelectionRuleCompareType { get; set; }
        public string Value { get; set; }

        public SelectionRule(int profileId, SelectionRuleParts selectionRulePart, SelectionRuleCompareTypes selectionRuleCompareType, string value)
        {
            ProfileId = profileId;
            SelectionRulePart = selectionRulePart;
            SelectionRuleCompareType = selectionRuleCompareType;
            Value = value;
        }

        public bool IsMatch(string urlToMatch)
        {
            bool? isMatch = false;
            var urlPart = GetPartFromUrl(SelectionRulePart, urlToMatch);
            var comparisonType = StringComparison.CurrentCultureIgnoreCase;

            switch (SelectionRuleCompareType)
            {
                case SelectionRuleCompareTypes.Equals:
                    isMatch = urlPart?.Equals(Value, comparisonType);
                    break;
                case SelectionRuleCompareTypes.Contains:
                    isMatch = urlPart?.Contains(Value, comparisonType);
                    break;
                case SelectionRuleCompareTypes.StartsWith:
                    isMatch = urlPart?.StartsWith(Value, comparisonType);
                    break;
                case SelectionRuleCompareTypes.EndsWith:
                    isMatch = urlPart?.EndsWith(Value, comparisonType);
                    break;
            }

            return isMatch ?? false;
        }
    }
}
