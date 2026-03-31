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
        a = new ReferenceError();
        RunNTimes(a, 5);
        Console.WriteLine("Fence Error:");
        a = new FenceError();
        RunNTimes(a, 5);
        Console.WriteLine("Load Error:");
        a = new LoadError();
        RunNTimes(a, 5);
        Console.WriteLine("Store Error");
        a = new StoreError();
        RunNTimes(a, 5);
        
        Console.WriteLine("Reference Time:");
        a = new Reference();
        RunNTimes(a, 5);
        Console.WriteLine("Fence Time:");
        a = new Fence();
        RunNTimes(a, 5);
        Console.WriteLine("Load Time:");
        a = new Load();
        RunNTimes(a, 5);
        Console.WriteLine("Store Time");
        a = new Store();
        RunNTimes(a, 5);
    }

    static void RunNTimes(ITest test, int n)
    {
        for (int i = 0; i < n; i++)
        {
            test.Run();
        }
    }
}