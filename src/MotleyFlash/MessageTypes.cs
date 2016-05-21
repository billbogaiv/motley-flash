namespace MotleyFlash
{
    public class MessageTypes : IMessageTypes
    {
        public const string ERROR = "Error";
        public const string INFORMATION = "Information";
        public const string NOTICE = "Notice";
        public const string SUCCESS = "Success";
        public const string WARNING = "Warning";

        public MessageTypes(
            string error = ERROR,
            string information = INFORMATION,
            string notice = NOTICE,
            string success = SUCCESS,
            string warning = WARNING)
        {
            Error = error;
            Information = information;
            Notice = notice;
            Success = success;
            Warning = warning;
        }

        public string Error { get; }
        public string Information { get; }
        public string Notice { get; }
        public string Success { get; }
        public string Warning { get; }
    }
}
