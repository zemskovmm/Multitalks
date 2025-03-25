public class SemaphoreExample
{
    // Create a SemaphoreSlim with a maximum of 3, allowing 3 threads at a time
    private static SemaphoreSlim _semaphore = new SemaphoreSlim(3);

    // Simulate customer behavior (critical section)
    static void PlaceOrder(string customerName)
    {
        Console.WriteLine($"{customerName} is trying to place an order...");

        // Wait for a spot in the semaphore
        _semaphore.Wait();
        try
        {
            Console.WriteLine($"{customerName} is placing an order...");
            Thread.Sleep(2000); // Simulate the time to place an order
            Console.WriteLine($"{customerName} finished placing an order.");
        }
        finally
        {
            // Release the semaphore for the next customer
            _semaphore.Release();
            Console.WriteLine($"{customerName} has left the ordering queue.");
        }
    }
}