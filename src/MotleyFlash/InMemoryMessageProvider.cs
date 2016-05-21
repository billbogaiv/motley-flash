namespace MotleyFlash
{
    public class InMemoryMessageProvider : IMessageProvider
    {
        protected object messages { get; set; }

        public object Get()
        {
            return messages;
        }

        public void Set(object messages)
        {
            this.messages = messages;
        }
    }
}
