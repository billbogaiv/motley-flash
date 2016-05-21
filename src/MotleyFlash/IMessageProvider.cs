namespace MotleyFlash
{
    public interface IMessageProvider
    {
        object Get();
        void Set(object messages);
    }
}
