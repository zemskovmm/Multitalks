using System;
using System.IO;
using System.Threading;

class MutexExample
{
    // Define a named Mutex for cross-process synchronization
    private static Mutex _mutex;

    // The unique name of the Mutex (shared between all processes)
    private const string MutexName = "Global\\MyMutexExample";

    static void Main(string[] args)
    {
        // Initialize the Mutex (if it doesn't exist, create it)
        bool isMutexOwner = false; // Indicates whether this process owns the mutex
        try
        {
            _mutex = new Mutex(false, MutexName, out isMutexOwner);

            if (isMutexOwner)
            {
                Console.WriteLine("Created a new named Mutex.");
            }
            else
            {
                Console.WriteLine("Opened an existing named Mutex.");
            }

            // Attempt to acquire the mutex
            Console.WriteLine("Waiting to acquire the Mutex...");
            _mutex.WaitOne();
            Console.WriteLine("Mutex acquired! Entering critical section.");

            // Critical Section: Simulate exclusive access to a shared resource
            AccessSharedResource();

            // Release the Mutex
            Console.WriteLine("Releasing the Mutex...");
            _mutex.ReleaseMutex();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Dispose of the Mutex resource
            if (_mutex != null)
            {
                _mutex.Dispose();
            }
        }

        Console.WriteLine("Application finished. Press any key to exit.");
        Console.ReadKey();
    }
}