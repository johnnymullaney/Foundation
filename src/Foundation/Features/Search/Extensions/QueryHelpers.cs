﻿using EPiServer.Find;
using EPiServer.Find.Api.Querying;
using EPiServer.Find.Api.Querying.Queries;
using System.Linq;
using System.Text.RegularExpressions;

namespace Foundation.Features.Search.Extensions
{

    public static class QueryHelpers
    {
        public static readonly Regex SpaceTabsRegex = new Regex(@"/\\s+/", RegexOptions.Compiled);
        public static readonly Regex TermsAndQuotedTermsRegex = new Regex(@"([\w-&´'`]+)|([""][\s\w-&´'`]+[""])", RegexOptions.Compiled);


        // Return all terms and phrases in query
        public static string[] GetQueryPhrases(string query)
        {
            // Replace double space, tabs with single whitespace and trim space on side
            string cleanedQuery = SpaceTabsRegex.Replace(query, " ").Trim();

            // Match single terms and quoted terms, allow -&´'` in terms, allow space between quotes and word.
            return TermsAndQuotedTermsRegex.Matches(cleanedQuery).Cast<Match>().Select(c => c.Value.Trim()).Except(new string[] { "AND", "OR", "NOT" }).Take(50).ToArray();
        }

        public static string UnescapeElasticSearchQuery(string s)
        {
            return s.Replace("\\", "");
        }

        public static string EscapeElasticSearchQuery(string s)
        {
            return s.Replace("-", "\\-");
        }

        public static bool GetFirstQueryStringQuery(ISearchContext context, out MinShouldMatchQueryStringQuery currentMinShouldMatchQueryStringQuery, out BoolQuery currentBoolQuery)
        {
            IQuery currentQuery;

            // Check for existing bool query with possible queries within and get the first which would be the one generated by For()
            if (QueryHelpers.TryGetBoolQuery(context.RequestBody.Query, out currentBoolQuery))
            {
                currentQuery = currentBoolQuery.Should.FirstOrDefault();
            }
            else
            {
                currentQuery = context.RequestBody.Query;
            }

            // Try get MinShouldMatchQueryStringQuery 
            if (TryGetMinShouldMatchQueryStringQuery(currentQuery, out currentMinShouldMatchQueryStringQuery))
            {
                return true;
            }

            // Try get MultiFiledQueryStringQuery and convert to MinShouldMatchQueryString because this is what all features in Toolbox work with
            if (TryGetMultiFieldQueryStringQuery(currentQuery, out MultiFieldQueryStringQuery currentMultiFieldQueryStringQuery))
            {
                // Convert to MinShouldMatchQueryStringQuery
                currentMinShouldMatchQueryStringQuery = new MinShouldMatchQueryStringQuery(currentMultiFieldQueryStringQuery.Query)
                {
                    RawQuery = currentMultiFieldQueryStringQuery.RawQuery,
                    AllowLeadingWildcard = currentMultiFieldQueryStringQuery.AllowLeadingWildcard,
                    AnalyzeWildcard = currentMultiFieldQueryStringQuery.AnalyzeWildcard,
                    Analyzer = currentMultiFieldQueryStringQuery.Analyzer,
                    AutoGeneratePhraseQueries = currentMultiFieldQueryStringQuery.AutoGeneratePhraseQueries,
                    Boost = currentMultiFieldQueryStringQuery.Boost,
                    DefaultOperator = currentMultiFieldQueryStringQuery.DefaultOperator,
                    EnablePositionIncrements = currentMultiFieldQueryStringQuery.EnablePositionIncrements,
                    FuzzyMinSim = currentMultiFieldQueryStringQuery.FuzzyMinSim,
                    FuzzyPrefixLength = currentMultiFieldQueryStringQuery.FuzzyPrefixLength,
                    LowercaseExpandedTerms = currentMultiFieldQueryStringQuery.LowercaseExpandedTerms,
                    PhraseSlop = currentMultiFieldQueryStringQuery.PhraseSlop,
                    DefaultField = currentMultiFieldQueryStringQuery.DefaultField,
                    Fields = currentMultiFieldQueryStringQuery.Fields
                };
                return true;
            }

            // Try get MultiFiledQueryStringQuery and convert to MinShouldMatchQueryString because this is what all features in Toolbox work with
            if (TryGetQueryStringQuery(currentQuery, out QueryStringQuery currentQueryStringQuery))
            {
                // Convert to MinShouldMatchQueryStringQuery
                currentMinShouldMatchQueryStringQuery = new MinShouldMatchQueryStringQuery(currentQueryStringQuery.Query)
                {
                    RawQuery = currentQueryStringQuery.RawQuery,
                    AllowLeadingWildcard = currentQueryStringQuery.AllowLeadingWildcard,
                    AnalyzeWildcard = currentQueryStringQuery.AnalyzeWildcard,
                    Analyzer = currentQueryStringQuery.Analyzer,
                    AutoGeneratePhraseQueries = currentQueryStringQuery.AutoGeneratePhraseQueries,
                    Boost = currentQueryStringQuery.Boost,
                    DefaultOperator = currentQueryStringQuery.DefaultOperator,
                    EnablePositionIncrements = currentQueryStringQuery.EnablePositionIncrements,
                    FuzzyMinSim = currentQueryStringQuery.FuzzyMinSim,
                    FuzzyPrefixLength = currentQueryStringQuery.FuzzyPrefixLength,
                    LowercaseExpandedTerms = currentQueryStringQuery.LowercaseExpandedTerms,
                    PhraseSlop = currentQueryStringQuery.PhraseSlop,
                    DefaultField = currentQueryStringQuery.DefaultField,
                    Fields = currentMultiFieldQueryStringQuery.Fields
                };
                return true;
            }

            return false;
        }

        public static bool TryGetMinShouldMatchQueryStringQuery(IQuery query, out MinShouldMatchQueryStringQuery currentMinShouldMatchQueryStringQuery)
        {
            currentMinShouldMatchQueryStringQuery = query as MinShouldMatchQueryStringQuery;
            if (currentMinShouldMatchQueryStringQuery == null)
            {
                return false;
            }
            return true;

        }

        public static bool TryGetMultiFieldQueryStringQuery(IQuery query, out MultiFieldQueryStringQuery currentMinShouldMatchQueryStringQuery)
        {

            currentMinShouldMatchQueryStringQuery = query as MultiFieldQueryStringQuery;
            if (currentMinShouldMatchQueryStringQuery == null)
            {
                return false;
            }
            return true;

        }

        public static bool TryGetQueryStringQuery(IQuery query, out QueryStringQuery currentQueryStringQuery)
        {
            currentQueryStringQuery = query as QueryStringQuery;
            if (currentQueryStringQuery == null)
            {
                return false;
            }
            return true;

        }

        public static bool TryGetBoolQuery(IQuery query, out BoolQuery currentBoolQuery)
        {
            currentBoolQuery = query as BoolQuery;
            if (currentBoolQuery == null)
            {
                currentBoolQuery = new BoolQuery();
                return false;
            }

            return true;

        }

        public static string GetRawQueryString(QueryStringQuery currentQueryStringQuery)
        {
            return (currentQueryStringQuery.RawQuery.Trim() ?? string.Empty).ToString();
        }
        public static bool IsStringQuoted(string text)
        {
            return (text.StartsWith("\"") && text.EndsWith("\""));
        }
    }

}