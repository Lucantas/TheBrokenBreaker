using System;
using System.Collections.Generic;
using System.Text;

namespace TheRealBrokenBreaker
{
    class CLI
    {
        public static void Run()
        {
            var option = 0;
            WriteMenu();
            option = int.Parse(Console.ReadLine());
            HandleOptions(option);
        }
        public static void Quit()
        {

        }
        private static void WriteMenu()
        {
            Console.WriteLine("##########################################################");
            Console.WriteLine("############  Welcome to the broken breaker  #############");
            Console.WriteLine("##########################################################");
            Console.WriteLine("##############  Choose an option to begin  ###############");
            Console.WriteLine("1 - Input URIs");
            Console.WriteLine("2 - Read inputs from xml");
            Console.WriteLine("3 - Read logs");
            Console.WriteLine("0 - Exit");
        }
        public static int HandleOptions(int option)
        {
            switch(option)
            {
                case 0:
                    Quit();
                    break;
                case 1:
                    ReadConsoleInputs();
                    break;
                case 2:
                    ReadFromFile();
                    break;
                case 3:
                    ShowLogs();
                    break;
            }
            return 0;
        }
        public static void ReadConsoleInputs()
        {
            string[] URIs;
            Console.WriteLine("Reading console from inputs");
            Console.WriteLine("Please insert the URIs separated by commas");
            URIs = Console.ReadLine().Split(",");
            foreach(var uri in URIs)
            {
                BrokenBreaker.GetBrokenLinks(uri);
            }
        }
        public static void ReadFromFile()
        {
            Console.WriteLine("Reading URIs from XML");
        }
        public static void ShowLogs()
        {
            Console.WriteLine("Preparing to show Logs");
        }
    }
}
