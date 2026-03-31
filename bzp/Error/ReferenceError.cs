using System.Diagnostics;

namespace bzp;

public class ReferenceError : TestError
{
    protected override void Thread1()
    {
        for (int i = 0; i < iterations; i++)
        {
            barrier.SignalAndWait();
            x = 1;
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
            b = x;
            barrier.SignalAndWait();
        }
    }
}