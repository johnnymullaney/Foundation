using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Forms.EditView.Models.Internal;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace Foundation.Features.Forms.Elements.SelectImage
{
    public class ImageOptionItem : OptionItem
    {
        [Display(Name = "Main Image", GroupName = SystemTabNames.Content)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Image { get; set; }
    }
}