using System;

namespace MotleyFlash
{
    public class Message
    {
        public Message(
            string text,
            string title = null,
            string type = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            Text = text;
            Title = title;
            Type = type;
        }

        public string Text { get; private set; }

        /// <summary>
        /// This property is intended to allow `IMessenger` implementations
        /// a way to validate ordering of messages when retrieving from `IMessageProvider`.
        /// Do not rely on this property for any purpose outside custom `IMessenger` implementation.
        /// </summary>
        public long MessengerOrderId { get; set; }

        public string Title { get; private set; }
        public string Type { get; private set; }
    }
}
