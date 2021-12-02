using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;

namespace Foundation.Features.Forms.Elements.SelectImage
{
    [PropertyDefinitionTypePlugIn]
    public class ImageOptionItemProperty : PropertyList<ImageOptionItem>
    {
        protected override ImageOptionItem ParseItem(string value)
        {
            return JsonConvert.DeserializeObject<ImageOptionItem>(value);
        }
    }
}