using System.Diagnostics;

namespace bzp;

public class Fence : TestTime
{
    protected override void Thread1()
    {
        barrier.SignalAndWait();
        for (int i = 0; i < 1000000; i++)
        {
            x = 1;
            Thread.MemoryBarrier();
            a = y;
        }
    }

    protected override void Thread2()
    {
        barrier.SignalAndWait();
        for (int i = 0; i < 1000000; i++)
        {
            y = 1;
            Thread.MemoryBarrier();
            b = x;
        }
    }
}