using EPiServer.Forms.Controllers;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using System.Web.Mvc;

namespace Foundation.Features.Forms
{

    [TemplateDescriptor(
        AvailableWithoutTag = true,
        Default = true,
        ModelType = typeof(DataCaptureContainerBlock),
        TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]
    public class DataCaptureContainerBlockController : FormContainerBlockController
    {
        public override ActionResult Index(FormContainerBlock currentBlock)
        {
            var partialResult = base.Index(currentBlock);

            if (currentBlock is DataCaptureContainerBlock dataCaptureContainerBlock)
            {
                return PartialView("~/Features/Shared/Views/ElementBlocks/DataCaptureContainerBlock.cshtml", dataCaptureContainerBlock);
            }

            return partialResult;
        }
    }
}