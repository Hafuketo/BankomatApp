using BankomatApp.Domain.Entities;
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

        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<long>("Your card number: ");
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("Enter your card PIN: "));

            return tempUserAccount;

        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\nChecking card number and PIN...");
            Utility.PrintDotAnimation();
        }

        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Your account is locked. Please go to the nearest branch to unlock your account.", true);
            Utility.PressEnterToContinue();
            Environment.Exit(1);
        }
    }
}
