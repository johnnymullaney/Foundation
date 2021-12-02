using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements.BaseClasses;
using EPiServer.Shell.ObjectEditing;
using System.Collections.Generic;

namespace Foundation.Features.Forms.Elements.SelectImage
{
    [ContentType(
        DisplayName = "Image Selection",
        GUID = "d9f2477f-3d53-441a-b5eb-75eebb7106b7",
        GroupName = "DataCaptureFormElements",
        Description = "Display a list of image and text options",
        Order = 2301)]
    //TODO: [ImageUrl("")]
    public class SelectImageElementBlock : SelectionElementBlockBase<ImageOptionItem>
    {
        [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<ImageOptionItem>))]
        public override IEnumerable<ImageOptionItem> Items { get; set; }
    }
}