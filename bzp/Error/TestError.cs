using System.Diagnostics;

namespace bzp;
public class TestError : ITest
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

        for (int i = 0; i < iterations; i++)
        {
            x = y = a = b = 0;
            
            barrier.SignalAndWait();
            
            barrier.SignalAndWait();

            result[a, b] += 1;
        }

        t1.Join();
        t2.Join();

        sw.Stop();
        Console.WriteLine($"{result[0,0]}\t{result[0,1]}\t{result[1,0]}\t{result[1,1]}\t{sw.Elapsed.TotalMilliseconds:F2}");
    }
}