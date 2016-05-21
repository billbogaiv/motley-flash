using System.Collections.Generic;
using System.Linq;

namespace MotleyFlash
{
    public class QueueMessenger : IMessenger
    {
        public QueueMessenger(
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
            var providerMessages = messageProvider.Get() as Queue<Message>
                ?? new Queue<Message>();

            message.MessengerOrderId = providerMessages.Count();

            var messages = SetMessageOrder(providerMessages);

            messages.Enqueue(message);
            messageProvider.Set(messages);

            return this;
        }

        public int Count()
        {
            var providerMessages = messageProvider.Get() as Queue<Message>
                ?? new Queue<Message>();

            return providerMessages.Count();
        }

        public Message Fetch()
        {
            var providerMessages = messageProvider.Get() as Queue<Message>
                ?? new Queue<Message>();

            var messages = SetMessageOrder(providerMessages);

            Message message = null;

            if (messages.Count > 0)
            {
                message = messages.Dequeue();
                messageProvider.Set(messages);
            }

            return message;
        }

        private Queue<Message> SetMessageOrder(IEnumerable<Message> unorderedMessages)
        {
            var messages = new Queue<Message>(
                unorderedMessages.OrderBy(x => x.MessengerOrderId));

            return messages;
        }

    }
}
