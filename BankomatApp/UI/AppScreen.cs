using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatApp.UI
{
    public static class AppScreen
    {
        internal static void Welcome()
        {
            // Clears screen
            Console.Clear();

            // Title of console window
            Console.Title = "Bankomaten";

            // Change text color
            Console.ForegroundColor = ConsoleColor.White;

            // Intro top text
            Console.WriteLine("\n\n----------------Välkommen till min bankomat----------------\n\n");

            // Prompt user to insert card
            Console.WriteLine("Vänligen sätt in ditt bankkort i automaten");
            Console.WriteLine("Obs!; En riktig automat skulle här begära och läsa av ett fysiskt kort");
            Utility.PressEnterToContinue();
        }
    }
}
