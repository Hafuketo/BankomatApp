using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatApp.UI
{
    public static class Utility
    {
        //Prints message with specified color
        public static void PrintMessage(string msg, bool success)
        {
            if (success)
            {
                // Used when input is correct
                Console.ForegroundColor = ConsoleColor.Yellow;


            } else
            {
                // Used when input is wrong
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(msg);
            Console.ResetColor();
            PressEnterToContinue();
        }

        public static string GetUserInput(string prompt)
        {
            Console.WriteLine($"Enter {prompt}");
            return Console.ReadLine();
        }

        public static void PressEnterToContinue()
        {
            Console.WriteLine("\n\nTryck på en aknapp för att fortsätta...\n");
            Console.ReadLine();
        }
    }
}
