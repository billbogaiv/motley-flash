# Motley Flash

Project is designed to provide base-level, broad compatibility across many .NET frameworks. Additionally, the project can contain high-level functionality for a smaller number of frameworks (i.e. ASP.NET Core 1.0).

## How does it work?

Motley Flash follows a design-principle known as "flash messaging". This allows the implementator to create one-time-use messages for UI-consumption. Messages are added to a queue and are designed to be retained between requests. 
Once the message is consumed it is removed from the queue.

The library has three key aspects: `Message`, `IMessenger`, and `IMessageProvider`. The first-item holds properties related to the individual message, the second-item controls how messages are stored and retrieved (i.e. FIFO or LIFO), while the last-item deals with how the collection of messages are stored (i.e. session or in-memory).

## What is the use case?

A common MVC-pattern is to `post` a `form` to an endpoint, process the data, then redirect the request to another endpoint via a `get`-request (i.e. [Post-Redirect-Get](https://en.wikipedia.org/wiki/Post/Redirect/Get)). Using Motley Flash, a message can be created within the `post`-action and retained until it is consumed by the latter `get`-request.

Another example is to create a series of messages (maybe just information or log-related) during an action and then displaying them based on some ordering (FIFO or LIFO).

Lastly, since the library is not tied to web applications, it could also be used in console or desktop applications.

## Examples

### ASP.NET Core 1.0

#### Startup.cs

```csharp
using MotleyFlash;
using MotleyFlash.AspNetCore.MessageProviders;

// Boilerplate...

public void ConfigureServices(IServiceCollection services)
{
    // Add framework services.
    services.AddMvc();

    services.AddSession();

    // Needed so we can access the user's session.
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddScoped(x => x.GetRequiredService<IHttpContextAccessor>().HttpContext.Session);

    services.AddScoped<IMessageProvider, SessionMessageProvider>();

    // Customize the message types (i.e. we are using Bootstrap v3 and need to provide a custom-value for the error message-type).
    services.AddScoped<IMessageTypes>(x =>
    {
        return new MessageTypes(error: "danger");
    });

    services.AddScoped<IMessengerOptions, MessengerOptions>();

    // We are using a stack to hold messages (i.e. LIFO).
    services.AddScoped<IMessenger, StackMessenger>();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    // Boilerplate...

    app.UseSession();

    app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

#### HomeController.cs

```csharp
using MotleyFlash;
using MotleyFlash.Extensions;

// Boilerplate...

public class HomeController : Controller
{
    public HomeController(IMessenger messenger)
    {
        this.messenger = messenger;
    }

    private readonly IMessenger messenger;

    public IActionResult Index()
    {
        // Leveraging extension method.
        messenger.Success(
            text: $"Hello at {DateTimeOffset.UtcNow}.",
            title: "Welcome to Motley Flash");

        // Or, if you want to construct the message manually.
        //messenger.Add(new Message(
        //    text: $"Hello at {DateTimeOffset.UtcNow}.",
        //    title: "Welcome to Motley Flash"));

        return View();
    }
}
```

#### Views/Shared/Components/MotleyFlash/Default.cshtml

```csharp
@model IEnumerable<MotleyFlash.Message>

@foreach (var message in Model)
{
    <div class="alert alert-@message.Type.ToLower()" role="alert">
        @if (!string.IsNullOrWhiteSpace(message.Title))
        {
            <strong>@message.Title</strong><text>&nbsp;</text>
        }
        @message.Text
    </div>
}
```

#### _ViewImports.cshtml

```csharp
@using MotleyFlash.AspNetCore.ViewHelpers
@using MotleyFlash.AspNetCore.ViewHelpers.Extensions
```

#### Index.cshtml (or _Layout.cshtml)

The view is using an extension method to leverage the `MotleyFlashViewComponent`. By default, this will use `Default.cshtml`. You can also pass a view-name.

```csharp
@await Component.MotleyFlash()
// @await Component.MotleyFlash("Bootstrap")
```

#### Index.cshtml (Alternative)

```csharp
@await Component.Invoke("MotleyFlash")
```
