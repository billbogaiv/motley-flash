namespace MotleyFlash
{
    public class MessengerOptions : IMessengerOptions
    {
        public MessengerOptions(IMessageTypes messageTypes)
        {
            MessageTypes = messageTypes;
        }

        public IMessageTypes MessageTypes { get; }
    }
}
