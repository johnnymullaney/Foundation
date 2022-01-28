using EPiServer.Find;
using EPiServer.PdfPreview.Models;
using EPiServer.Web;
using Foundation.Features.CatalogContent.Product;
using Foundation.Features.Media;
using System.Web;

namespace Foundation.Features.Search
{
    public class ContentSearchViewModel
    {
        public SearchResults<GenericProduct> ProductHits { get; set; }

        public SearchResults<FoundationPdfFile> PdfHits { get; set; }

        public FilterOptionViewModel FilterOption { get; set; }

        public string SectionFilter => HttpContext.Current.Request.QueryString["t"] ?? string.Empty;

        public string GetSectionGroupUrl(string groupName)
        {
            var url = UriUtil.AddQueryString(HttpContext.Current.Request.RawUrl, "t", HttpContext.Current.Server.UrlEncode(groupName));
            url = UriUtil.AddQueryString(url, "p", "1");
            return url;
        }
    }
}