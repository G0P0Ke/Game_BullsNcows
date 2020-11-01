using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            while (true)
            {
                Console.Write(rand.Next(0,10));
            }
        }
    }
}