using System;
using System.Collections.Generic;
using System.Text;
using log4net.Config;

namespace Arp.Generator
{
    class Program
    {
        static void Main(string[] args)
        {

            if(args.Length != 3)
            {
                PrintUsage();
                return;
            }

            BasicConfigurator.Configure();

            Runner runner = new Runner();

            runner.OutputDirectory = args[0];
            runner.XsdFile = args[1];
            runner.BaseNamespace = args[2];

            runner.Run();
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("Arp.Generator <output directory> <xsd file> <base namepsace>");    
        }

    }
}
