using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace MotleyFlash.AspNetCore.ViewHelpers.Extensions
{
    public static class IViewComponentHelperExtensions
    {
        public async static Task<IHtmlContent> MotleyFlash(
            this IViewComponentHelper value,
            string view = null)
        {
            if (string.IsNullOrWhiteSpace(view))
            {
                return await value.InvokeAsync<MotleyFlashViewComponent>(new MotleyFlashViewComponentOptions());
            }
            else
            {
                return await value.InvokeAsync<MotleyFlashViewComponent>(new MotleyFlashViewComponentOptions()
                {
                    View = view
                });
            }
        }
    }
}
