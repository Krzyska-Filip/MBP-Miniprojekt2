using System.Diagnostics;

namespace bzp;

public class Load : TestTime
{
    protected override void Thread1()
    {
        barrier.SignalAndWait();
        for (int i = 0; i < iterations; i++)
        {
            x = 1;
            a = Interlocked.Read(ref y);
        }
    }

    protected override void Thread2()
    {
        barrier.SignalAndWait();
        for (int i = 0; i < iterations; i++)
        {
            y = 1;
            b = Interlocked.Read(ref x);
        }
    }
}