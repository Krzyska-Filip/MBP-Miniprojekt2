using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace bzp;

public class LoadError : TestError
{
    protected override void Thread1()
    {
        for (int i = 0; i < iterations; i++)
        {
            barrier.SignalAndWait();
            x = 1;
            a = Interlocked.Read(ref y);
            barrier.SignalAndWait();
        }
    }
    
    protected override void Thread2()
    {
        for (int i = 0; i < iterations; i++)
        {
            barrier.SignalAndWait();
            y = 1;
            b = Interlocked.Read(ref x);
            barrier.SignalAndWait();
        }
    }
}