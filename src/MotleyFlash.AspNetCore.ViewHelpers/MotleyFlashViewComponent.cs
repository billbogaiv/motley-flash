using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MotleyFlash.AspNetCore.ViewHelpers
{
    public class MotleyFlashViewComponent : ViewComponent
    {
        public MotleyFlashViewComponent(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        private readonly IMessenger messenger;

        public IViewComponentResult Invoke()
        {
            var messages = new List<Message>();

            while (messenger.Count() > 0)
            {
                messages.Add(messenger.Fetch());
            }

            var options = new MotleyFlashViewComponentOptions();

            if (ViewComponentContext.Arguments.ContainsKey(nameof(options.View)))
            {
                return View(ViewComponentContext.Arguments[nameof(options.View)].ToString(), messages);
            }
            else
            {
                return View(messages);
            }
        }
    }
}
