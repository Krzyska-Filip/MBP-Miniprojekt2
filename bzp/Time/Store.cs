using System.Diagnostics;

namespace bzp;

public class Store : TestTime
{
    protected override void Thread1()
    {
        barrier.SignalAndWait();
        for (int i = 0; i < iterations; i++)
        {
            Interlocked.Exchange(ref x, 1);
            a = y;
        }
    }

    protected override void Thread2()
    {
        barrier.SignalAndWait();
        for (int i = 0; i < iterations; i++)
        {
            Interlocked.Exchange(ref y, 1);
            b = x;
        }
    }
}