namespace DataCollector.Service
{
    public static class ConsoleLogService
    {
        public static void LogSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCCESS] {DateTime.Now:dd-MM-yyyy HH:mm:ss} - {message}");
            Console.ResetColor();
        }

        public static void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {DateTime.Now:dd-MM-yyyy HH:mm:ss} - {message}");
            Console.ResetColor();
        }

        public static void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[INFO] {DateTime.Now:dd-MM-yyyy HH:mm:ss} - {message}");
            Console.ResetColor();
        }

        public static void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARNING] {DateTime.Now:dd-MM-yyyy HH:mm:ss} - {message}");
            Console.ResetColor();
        }
    }
}
