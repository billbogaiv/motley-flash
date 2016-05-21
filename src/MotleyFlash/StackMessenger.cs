using System.Collections.Generic;
using System.Linq;

namespace MotleyFlash
{
    public class StackMessenger : IMessenger
    {
        public StackMessenger(
            IMessageProvider messageProvider,
            IMessengerOptions messengerOptions)
        {
            this.messageProvider = messageProvider;

            Options = messengerOptions;
        }

        private readonly IMessageProvider messageProvider;
        public IMessengerOptions Options { get; }

        public IMessenger Add(Message message)
        {
            var providerMessages = messageProvider.Get() as Stack<Message>
                ?? new Stack<Message>();

            message.MessengerOrderId = providerMessages.Count();

            var messages = SetMessageOrder(providerMessages);

            messages.Push(message);
            messageProvider.Set(messages);

            return this;
        }

        public int Count()
        {
            var providerMessages = messageProvider.Get() as Stack<Message>
                ?? new Stack<Message>();

            return providerMessages.Count();
        }

        public Message Fetch()
        {
            var providerMessages = messageProvider.Get() as Stack<Message>
                ?? new Stack<Message>();

            var messages = SetMessageOrder(providerMessages);

            Message message = null;

            if (messages.Count > 0)
            {
                message = messages.Pop();
                messageProvider.Set(messages);
            }

            return message;
        }

        private Stack<Message> SetMessageOrder(IEnumerable<Message> unorderedMessages)
        {
            var messages = new Stack<Message>(
                unorderedMessages.OrderBy(x => x.MessengerOrderId));

            return messages;
        }
    }
}
