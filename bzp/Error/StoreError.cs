using System.Diagnostics;

namespace bzp;

public class StoreError : TestError
{
    protected override void Thread1()
    {
        for (int i = 0; i < iterations; i++)
        {
            barrier.SignalAndWait();
            Interlocked.Exchange(ref x, 1);
            a = y;
            barrier.SignalAndWait();
        }
    }

    protected override void Thread2()
    {
        for (int i = 0; i < iterations; i++)
        {
            barrier.SignalAndWait();
            Interlocked.Exchange(ref y, 1);
            b = x;
            barrier.SignalAndWait();
        }
    }
}