public class CrossProcessSemaphoreExample
{
    private const string SemaphoreName = "Global\\MyCrossProcessSemaphore";
    private static Semaphore _semaphore;

    static void Main(string[] args)
    {
        Console.WriteLine("Application started. Press Enter to acquire the semaphore.");
        Console.ReadLine();

        // Initialize the named semaphore (max 3 concurrent processes)
        _semaphore = new Semaphore(3, 3, SemaphoreName, out bool createdNew);

        Console.WriteLine(createdNew ? "Created new global semaphore." : "Opened existing global semaphore.");

        try
        {
            // Acquire the semaphore
            Console.WriteLine("Waiting for semaphore...");
            _semaphore.WaitOne();
            Console.WriteLine("Semaphore acquired! Writing to log file...");

            // Critical section: Write to a shared file
            WriteToLogFile();

            Console.WriteLine("Finished writing. Releasing semaphore...");
        }
        finally
        {
            // Release the semaphore
            _semaphore.Release();
            Console.WriteLine("Semaphore released.");
        }

        Console.WriteLine("Application finished. Press any key to exit.");
        Console.ReadKey();
    }

    static void WriteToLogFile()
    {
        const string logFilePath = "SharedLogFile.txt";

        using (StreamWriter writer = File.AppendText(logFilePath))
        {
            writer.WriteLine($"{DateTime.Now}: Process {Environment.ProcessId} wrote to the log.");
        }
    }
}