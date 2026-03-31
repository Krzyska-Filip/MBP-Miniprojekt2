using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace bzp;

public class FenceError : TestError
{
    protected override void Thread1()
    {
        for (int i = 0; i < iterations; i++)
        {
            barrier.SignalAndWait();
            x = 1;
            Thread.MemoryBarrier();
            a = y;
            barrier.SignalAndWait();
        }
    }
    
    protected override void Thread2()
    {
        for (int i = 0; i < iterations; i++)
        {
            barrier.SignalAndWait();
            y = 1;
            Thread.MemoryBarrier();
            b = x;
            barrier.SignalAndWait();
        }
    }
}