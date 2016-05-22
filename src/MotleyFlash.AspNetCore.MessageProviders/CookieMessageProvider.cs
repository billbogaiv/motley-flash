using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MotleyFlash.AspNetCore.MessageProviders
{
    public class CookieMessageProvider : IMessageProvider
    {
        public CookieMessageProvider(
            IRequestCookieCollection request,
            IResponseCookies response)
        {
            this.response = response;

            string value;

            if (request.TryGetValue(cookieKey, out value))
            {
                if (value != null)
                {
                    messages = JsonConvert.DeserializeObject(value, jsonSerializerSettings);
                }
            }
        }

        private const string cookieKey = "motleyflash-cookie-message-provider";

        private static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        private object messages = null;
        private readonly IResponseCookies response;

        public object Get()
        {
            return messages;
        }

        public void Set(object messages)
        {
            this.messages = messages;

            response.Append(
                cookieKey,
                JsonConvert.SerializeObject(
                    messages,
                    jsonSerializerSettings));
        }
    }
}
