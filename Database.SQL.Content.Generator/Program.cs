using Database.SQL.Content.Generator.Configuration;
using Database.SQL.Content.Generator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQL.Content.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var configuration = ConfigurationLoader.Load();
                var generator = new ContentGenerator(configuration, Console.Out);
                generator.StartAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!");
                Console.WriteLine("Message:");
                Console.WriteLine($"\t{ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine("InnerMessage:");
                    Console.WriteLine($"\t{ex.InnerException.Message}");

                    if (ex.InnerException.InnerException != null)
                    {
                        Console.WriteLine("InnerMessage2:");
                        Console.WriteLine($"\t{ex.InnerException.InnerException.Message}");
                    }
                }
            }

            Console.WriteLine("Process completed");
            Console.ReadLine();
        }
    }
}
