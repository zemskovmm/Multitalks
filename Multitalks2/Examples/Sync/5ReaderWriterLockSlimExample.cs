public class ReaderWriterLockSlimExample
{
    private static ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
    private static string sharedData = "Initial Value";

    public static void Example()
    {
        Thread reader1 = new Thread(ReaderThread);
        Thread reader2 = new Thread(ReaderThread);
        Thread writer = new Thread(WriterThread);

        reader1.Start();
        writer.Start();
        reader2.Start();

        reader1.Join();
        writer.Join();
        reader2.Join();
    }

    public static void ExampleInconsistentData()
    {
        Thread[] writers = new Thread[5];
        for (int i = 0; i < writers.Length; i++)
        {
            int threadId = i + 1;
            writers[i] = new Thread(() => WriterThread(threadId));
            writers[i].Start();
        }

        foreach (var thread in writers)
        {
            thread.Join();
        }

        Console.WriteLine($"Final Data: {sharedData}");
    }

    public static void ExampleConsistentData()
    {
        // Create threads to simulate concurrent reading and writing
        Thread[] writers = new Thread[5];
        for (int i = 0; i < writers.Length; i++)
        {
            int threadId = i + 1;
            writers[i] = new Thread(() => WriterThread(threadId));
        }

        Thread reader = new Thread(ReaderThread);

        // Start all writer threads
        foreach (var writer in writers)
        {
            writer.Start();
        }

        reader.Start(); // Start a reader thread

        // Wait for all threads to complete
        foreach (var writer in writers)
        {
            writer.Join();
        }
        reader.Join();

        Console.WriteLine($"Final Data: {sharedData}");
    }

    static void ReaderThread()
    {
        rwLock.EnterReadLock();
        try
        {
            Console.WriteLine($"[Reader] Reading Shared Data: {sharedData}");
        }
        finally
        {
            rwLock.ExitReadLock();
        }
    }

    static void WriterThread()
    {
        rwLock.EnterWriteLock();
        try
        {
            sharedData = $"Updated at {DateTime.Now}";
            Console.WriteLine($"[Writer] Updated Shared Data: {sharedData}");
        }
        finally
        {
            rwLock.ExitWriteLock();
        }
    }

    static void WriterThread(int threadId)
    {
        // Each thread writes to the shared resource without any synchronization
        sharedData = $"Written by Thread-{threadId}";
        Console.WriteLine(sharedData);
    }

    public void ReadAndMaybeWriteData()
    {
        ReaderWriterLockSlim rwLockSlim = new ReaderWriterLockSlim();
        var needsWrite = true;
        rwLockSlim.EnterUpgradeableReadLock();
        try
        {
            if (needsWrite)
            {
                rwLockSlim.EnterWriteLock();
                try
                {
                    // Perform write operation
                }
                finally
                {
                    rwLockSlim.ExitWriteLock();
                }
            }
        }
        finally
        {
            rwLockSlim.ExitUpgradeableReadLock();
        }
    }
}
