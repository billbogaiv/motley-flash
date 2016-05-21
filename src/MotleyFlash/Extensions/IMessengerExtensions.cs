namespace MotleyFlash.Extensions
{
    public static class IMessengerExtensions
    {
        public static IMessenger Error(
            this IMessenger value,
            string text,
            string title = null)
        {
            return value.AddMessage(text, value.Options.MessageTypes.Error, title);
        }

        public static IMessenger Information(
            this IMessenger value,
            string text,
            string title = null)
        {
            return value.AddMessage(text, value.Options.MessageTypes.Information, title);
        }

        public static IMessenger Notice(
            this IMessenger value,
            string text,
            string title = null)
        {
            return value.AddMessage(text, value.Options.MessageTypes.Notice, title);
        }

        public static IMessenger Success(
            this IMessenger value,
            string text,
            string title = null)
        {
            return value.AddMessage(text, value.Options.MessageTypes.Success, title);
        }

        public static IMessenger Warning(
            this IMessenger value,
            string text,
            string title = null)
        {
            return value.AddMessage(text, value.Options.MessageTypes.Warning, title);
        }

        private static IMessenger AddMessage(
            this IMessenger value,
            string text,
            string type,
            string title = null)
        {
            value.Add(new Message(text, title, type));

            return value;
        }
    }
}
