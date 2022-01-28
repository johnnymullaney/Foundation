using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Foundation.Cms.Settings;
using Foundation.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Foundation.Features.Search.Search
{
    [SettingsContentType(DisplayName = "Search Algorithm Settings Page",
        GUID = "e9595195-2082-4d88-9543-75afa35a4498",
        Description = "Search Algorithm Settings Page",
        SettingsName = "Search Algorithm")]
    public class SearchAlgorithmSettingsPage : SettingsBase
    {
        [CultureSpecific]
        [Display(Name = "Minimum Should Match", GroupName = TabNames.QueryOptimisation, Order = 100)]
        public virtual string MinimumShouldMatch { get; set; }

        [CultureSpecific]
        [Display(Name = "Enable Display Name Fuzzy Match", GroupName = TabNames.QueryOptimisation, Order = 110)]
        public virtual bool EnableDisplayNameFuzzyMatch { get; set; }

        [CultureSpecific]
        [Display(Name = "Enable Display Name Wildcard Match", GroupName = TabNames.QueryOptimisation, Order = 120)]
        public virtual bool EnableDisplayNameWildcardMatch { get; set; }

        [CultureSpecific]
        [Display(Name = "Enable Synonyms Improved", GroupName = TabNames.QueryOptimisation, Order = 130)]
        public virtual bool EnableSynonymsImproved { get; set; }

        [CultureSpecific]
        [Display(Name = "Enable Display Name Relevance Improved", GroupName = TabNames.QueryOptimisation, Order = 140)]
        public virtual bool EnableDisplayNameRelevanceImproved { get; set; }

        [CultureSpecific]
        [Display(Name = "Display Name Weighting", GroupName = TabNames.PropertyWeightings, Order = 150)]
        public virtual int DisplayNameWeighting { get; set; }

        [CultureSpecific]
        [Display(Name = "Brand Weighting", GroupName = TabNames.PropertyWeightings, Order = 160)]
        public virtual int BrandWeighting { get; set; }

        [CultureSpecific]
        [Display(Name = "Description Weighting", GroupName = TabNames.PropertyWeightings, Order = 170)]
        public virtual int DescriptionWeighting { get; set; }

        [CultureSpecific]
        [Display(Name = "Popular Product Boosting Value", GroupName = TabNames.Boosting, Order = 180)]
        public virtual int PopularProductBoostingValue { get; set; }

        [CultureSpecific]
        [Display(Name = "Is Default", GroupName = TabNames.Experimentation, Order = 200)]
        public virtual bool IsDefault { get; set; }

        [CultureSpecific]
        [Display(Name = "Experiment Key", GroupName = TabNames.Experimentation, Order = 210)]
        public virtual string ExperimentKey { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {

            MinimumShouldMatch = "2<50%";
            EnableDisplayNameFuzzyMatch = true;
            EnableSynonymsImproved = true;
            EnableDisplayNameRelevanceImproved = true;
            EnableDisplayNameWildcardMatch = true;
            DisplayNameWeighting = 60;
            BrandWeighting = 40;
            DescriptionWeighting = 20;
            PopularProductBoostingValue = 500;
            IsDefault = false;

            base.SetDefaultValues(contentType);
        }
    }
}