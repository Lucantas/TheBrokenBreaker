using System;
using System.IO;
using System.Collections.Generic;
using TheRealBrokenBreaker.Models;

namespace TheRealBrokenBreaker
{
    class ConsoleInterface
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
            Environment.Exit(0);
        }
        private static void WriteMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("##########################################################");
            Console.WriteLine("############  Welcome to the broken breaker  #############");
            Console.WriteLine("##########################################################");
            Console.ResetColor();
            Console.WriteLine("##############  Choose an option to begin  ###############");
            Console.WriteLine("1 - Input URIs");
            Console.WriteLine("2 - Read inputs from file");
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
            Console.WriteLine("Reading URIs from File");
            // Create a empty list of string that will keep the links found in the text file
            var URIs = new List<string>();

            // // Define the directory where the program should look for the file

            // open the file with name "links.txt" and read it line by line
            try
            {
                using (StreamReader sr = new StreamReader(AppConfiguration.LinkFile))
                {
                    string line;
                    // test every line to see if it contains text
                    while ((line = sr.ReadLine()) != null)
                    {
                        // if the line match a url, add it to the Array
                        URIs.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            foreach(var uri in URIs)
            {
                BrokenBreaker.GetBrokenLinks(uri);
            }
        }
        public static void ShowLogs()
        {
            Console.WriteLine("Preparing to show Logs");
        }
    }
}
