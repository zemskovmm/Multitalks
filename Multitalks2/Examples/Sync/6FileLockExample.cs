public class FileLockExample
{
    private static string filePath = "SharedLog.txt";

    static void WriteToFile(string content)
    {
        try
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Attempting to lock file...");

            // Open the file for writing with FileShare.None (exclusive lock)
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    // Simulate work to show locking behavior
                    Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] File locked. Writing data...");
                    writer.WriteLine($"[{DateTime.Now}] {content}");
                    Thread.Sleep(2000); // Simulate file write delay
                    Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Done writing. Releasing lock.");
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] File is locked by another thread or process. Error: {ex.Message}");
        }
    }

    public static void Example()
    {
        // Create multiple threads to simulate file locking
        Thread thread1 = new Thread(() => WriteToFile("Thread 1 - Hello, from Thread 1!"));
        Thread thread2 = new Thread(() => WriteToFile("Thread 2 - Hello, from Thread 2!"));
        Thread thread3 = new Thread(() => WriteToFile("Thread 3 - Hello, from Thread 3!"));

        thread1.Start();
        thread2.Start();
        thread3.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();

        Console.WriteLine("All threads have completed their file access.");Í
    }
}