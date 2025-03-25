using System.Diagnostics;

public class Program
{
    public static int Main()
    {
        SyncFixture();

        DeadlockFixture();

        Thread.Sleep(20000);
        System.Console.WriteLine("That`s all folks!");
        return 0;
    }

    private static void DeadlockFixture()
    {
        var sw = new Stopwatch();

        sw.Start();
        Deadlock1();
        sw.Stop();
        System.Console.WriteLine($"It took {sw.ElapsedMilliseconds}ms to run the section.");


        /// STA Deadlock
        Console.WriteLine("Starting Main()");
        var result = DeadlockSTAExample().Result; // Blocking on an async method
        Console.WriteLine(result);
    }

    static async Task<string> DeadlockSTAExample()
    {
        Console.WriteLine("Starting DeadlockExample()");
        await Task.Delay(1000); // Asynchronous operation
        return "Hello, World!";
    }

    private static void SyncFixture()
    {
        var example = new MutexExample();

        Parallel.For(0, 100, i => example.Add(i));
    }

    private static void Deadlock1()
    {
        var circleDeadlock = new CircleDeadlock();

        circleDeadlock.Thread1(null);
        circleDeadlock.Thread2(null);
    }

    private static void Deadlock2()
    {
        var circleDeadlock = new CircleDeadlock();

        var threadOne = System.Threading.ThreadPool.QueueUserWorkItem(circleDeadlock.Thread1!);
        var threadTwo = System.Threading.ThreadPool.QueueUserWorkItem(circleDeadlock.Thread2!);
    }
}