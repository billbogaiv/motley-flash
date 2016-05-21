namespace MotleyFlash
{
    public interface IMessenger
    {
        IMessengerOptions Options { get; }

        IMessenger Add(Message message);
        int Count();
        Message Fetch();
    }
}
