using System;

namespace Valtec
{
    class Program
    {
        static void Main(string[] args)
        {
            Valtec.scr.Connect.OpenCOMPort();
            Console.WriteLine("Hello World!");
        }
    }
}