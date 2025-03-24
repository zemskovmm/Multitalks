public class CircleDeadlock
{
    private object lock1 = new object();
    private object lock2 = new object();

    public void Thread1(Object stateInfo)
    {
        lock (lock1)
        {
            Console.WriteLine($"Thread1 acquired lock1 on {Thread.CurrentThread.ManagedThreadId}");
            
            Thread.Sleep(1000); // Working hard
            lock (lock2)
            {
                Console.WriteLine($"Thread1 acquired lock1 and lock2 on {Thread.CurrentThread.ManagedThreadId}");
            }
        }
    }

    public void Thread2(Object stateInfo)
    {
        lock (lock2)
        {
            Console.WriteLine($"Thread2 acquired lock2 on {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000); // Working hard
            lock (lock1)
            {
                Console.WriteLine($"Thread2 acquired lock2 and lock1 on {Thread.CurrentThread.ManagedThreadId}");
            }
        }
    }
}