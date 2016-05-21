namespace MotleyFlash
{
    public interface IMessageTypes
    {
        string Error { get; }
        string Information { get; }
        string Notice { get; }
        string Success { get; }
        string Warning { get; }
    }
}
