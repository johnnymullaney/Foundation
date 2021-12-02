using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Core;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace Foundation.Features.Forms
{
    [ContentType(
        DisplayName = "Data Capture Form Container",
        GUID = "16660C0A-5129-4E6C-85C0-E16161842F2D",
        GroupName = "DataCaptureContainerElements")]
    [ServiceConfiguration(typeof(IFormContainerBlock))]
    public class DataCaptureContainerBlock : FormContainerBlock
    {
        [Display(Name = "Main Image", GroupName = SystemTabNames.Content)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference MainImage { get; set; }
    }
}