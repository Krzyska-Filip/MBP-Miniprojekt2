using System;
using System.Threading;
using System.Diagnostics;
using bzp;

class Program
{
    static void Main()
    {
        ITest a;
        
        Console.WriteLine("Reference Error:");
        RunNTimes(new ReferenceError(), 5);
        
        Console.WriteLine("Fence Error:");
        RunNTimes(new FenceError(), 5);
        
        Console.WriteLine("Load Error:");
        RunNTimes(new LoadError(), 5);
        
        Console.WriteLine("Store Error");
        RunNTimes(new StoreError(), 5);
        
        Console.WriteLine("Reference Time:");
        RunNTimes(new Reference(), 5);
        
        Console.WriteLine("Fence Time:");
        RunNTimes(new Fence(), 5);
        
        Console.WriteLine("Load Time:");
        RunNTimes(new Load(), 5);
        
        Console.WriteLine("Store Time");
        RunNTimes(new Store(), 5);
    }

    static void RunNTimes(ITest test, int n)
    {
        for (int i = 0; i < n; i++)
        {
            test.Run();
        }
    }
}