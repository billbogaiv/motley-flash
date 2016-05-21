using MotleyFlash;
using MotleyFlash.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MotleyFlash.AspNetCore.Mvc
{
    public class HomeController : Controller
    {
        public HomeController(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        private readonly IMessenger messenger;

        public IActionResult Index(bool throwException = false)
        {
            if (throwException)
            {
                //messenger.Add(new Message(
                //    text: $"Application exception occured @ {DateTimeOffset.UtcNow}.",
                //    title: "Error"));

                messenger.Error(
                    text: $"Application exception occured @ {DateTimeOffset.UtcNow}.",
                    title: "Error");

                throw new Exception("A message has been created. Go back to the previous page and refresh to see the message.");
            }

            return View();
        }

        public IActionResult Post()
        {
            //messenger.Add(new Message(
            //    text: $"Form posted @ {DateTimeOffset.UtcNow}. Going to redirect.",
            //    title: "Form post successful"));

            messenger.Success(
                text: $"Form posted @ {DateTimeOffset.UtcNow}. Going to redirect.",
                title: "Form post successful");

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
