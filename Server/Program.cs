namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Listener listener = new Listener();
            listener.Run();
        }
    }
}
