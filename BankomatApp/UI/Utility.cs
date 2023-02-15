using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatApp.UI
{
    public static class Utility
    {
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
