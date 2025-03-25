class SpinInterLock
{
    private int isLocked = 0;

    public void Enter()
    {
        while (Interlocked.CompareExchange(ref isLocked, 1, 0) != 0)
        {
            // Busy-wait until the lock is available
        }
    }

    public void Exit()
    {
        Interlocked.Exchange(ref isLocked, 0);
    }
}

public class SpinInterlockexample
{
    SpinInterLock spinLock = new SpinInterLock();

    void Example()
    {
        spinLock.Enter();
        try
        {
            // Simulate critical section
            Console.WriteLine("Thread entered critical section.");
            Thread.Sleep(100);
        }
        finally
        {
            spinLock.Exit();
            Console.WriteLine("Thread exited critical section.");
        }
    }
}