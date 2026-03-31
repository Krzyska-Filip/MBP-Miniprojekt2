using System.Diagnostics;

namespace bzp;

public class Reference : TestTime
{
    protected override void Thread1()
    {
        barrier.SignalAndWait();
        for (int i = 0; i < iterations; i++)
        {
            x = 1;
            a = y;
        }
    }

    protected override void Thread2()
    {
        barrier.SignalAndWait();
        for (int i = 0; i < iterations; i++)
        {
            y = 1;
            b = x;
        }
    }
}