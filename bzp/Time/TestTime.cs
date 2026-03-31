using System.Diagnostics;

namespace bzp;

public class TestTime : ITest
{
    protected long x = 0, y = 0;
    protected long a = 0, b = 0;
    protected Barrier barrier = new Barrier(3);
    protected const long iterations = 1_000_000;

    protected virtual void Thread1() { }
    protected virtual void Thread2() { }
    
    public void Run()
    {
        int[,] result = new int[2,2];
        Stopwatch sw = Stopwatch.StartNew();
        
        Thread t1 = new Thread(Thread1);
        Thread t2 = new Thread(Thread2);

        t1.Start();
        t2.Start();
        
        barrier.SignalAndWait();

        t1.Join();
        t2.Join();

        sw.Stop();
        Console.WriteLine($"{sw.Elapsed.TotalMilliseconds:F2}");
    }
}