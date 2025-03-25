public class RequestCounterExample
{
    // Define a shared atomic counter for tracking the number of requests
    private static int requestCount = 0;

    // Simulate processing a single request
    static void ProcessRequest()
    {
        // Increment the request counter atomically
        int currentCount = Interlocked.Increment(ref requestCount);

        // Simulate some work related to handling the request
        Console.WriteLine($"Processing request {currentCount}...");
        Thread.Sleep(new Random().Next(100, 500)); // Simulate processing delay

        Console.WriteLine($"Request {currentCount} processed.");
    }

    public async static void Example()
    {
        Console.WriteLine("Starting request processing...");

        // Simulate multiple threads handling requests
        Task[] tasks = new Task[10];
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = Task.Run(() => ProcessRequest());
        }

        // Wait for all tasks to complete
        await Task.WhenAll(tasks);

        // Output the total number of requests processed
        Console.WriteLine($"All requests processed. Total requests: {requestCount}");
    }
}