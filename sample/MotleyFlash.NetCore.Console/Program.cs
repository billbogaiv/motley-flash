namespace MotleyFlash.NetCore.Console
{
    public class Program
    {
        private static IMessenger messenger;
        private static MessageTypes messageTypes = new MessageTypes();

        public static void Main(string[] args)
        {
            var messageProvider = new InMemoryMessageProvider();
            var messengerOptions = new MessengerOptions(messageTypes);

            messenger = new StackMessenger(messageProvider, messengerOptions);

            var input = string.Empty;

            do
            {
                PrintHeader();

                input = System.Console.ReadLine();

                var now = System.DateTime.UtcNow;

                if (input == "e")
                {
                    messenger.Add(new Message("An error has occured", now.ToString(), messageTypes.Error));
                }
                else if (input == "i")
                {
                    messenger.Add(new Message("This is an information message.", now.ToString(), messageTypes.Information));
                }
                else if (input == "s")
                {
                    messenger.Add(new Message("Congraulations!", now.ToString(), messageTypes.Success));
                }
                else if (input == "w")
                {
                    messenger.Add(new Message("Be careful.", now.ToString(), messageTypes.Warning));
                }
                else if (input == "p")
                {
                    PrintMessages();
                }

                System.Console.WriteLine("");
            } while (input != "q");
        }

        public static void PrintHeader()
        {
            System.Console.WriteLine("(e) Add error message");
            System.Console.WriteLine("(i) Add informational message");
            System.Console.WriteLine("(s) Add success message");
            System.Console.WriteLine("(w) Add warning message");
            System.Console.WriteLine("(p) Print messages");
            System.Console.WriteLine("(q) Quit");
            System.Console.WriteLine("-----------------------------");
            System.Console.Write("What is your selection? ");
        }

        public static void PrintMessages()
        {
            var message = messenger.Fetch();

            while (message != null)
            {
                if (message.Type == messageTypes.Error)
                {
                    System.Console.ForegroundColor = System.ConsoleColor.Red;
                }
                else if (message.Type == messageTypes.Information)
                {
                    System.Console.ForegroundColor = System.ConsoleColor.Blue;
                }
                else if (message.Type == messageTypes.Success)
                {
                    System.Console.ForegroundColor = System.ConsoleColor.Green;
                }
                else if (message.Type == messageTypes.Warning)
                {
                    System.Console.ForegroundColor = System.ConsoleColor.Yellow;
                }

                System.Console.WriteLine($"[{message.Type}] {message.Title}: {message.Text}");

                System.Console.ResetColor();

                message = messenger.Fetch();
            }
        }
    }
}
