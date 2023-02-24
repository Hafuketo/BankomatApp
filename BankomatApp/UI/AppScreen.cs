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

            tempUserAccount.CardNumber = Validator.Convert<long>("your card number.");
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("Enter your card PIN"));
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
            Utility.PrintMessage("Your account is locked. Please go to the nearest branch to unlock your account.", "yellow");
            Environment.Exit(1);
        }

        internal static void WelcomeCustomer(string fullName)
        {
            Console.WriteLine($"Välkommen tillbaka, {fullName}");
            Utility.PressEnterToContinue();
        }

        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("#---My ATM App Menu----#");
            Console.WriteLine("|                      |");
            Console.WriteLine("| 1. Account Balance   |");
            Console.WriteLine("| 2. Insägttning       |");
            Console.WriteLine("| 3. Uttag             |");
            Console.WriteLine("| 4. Flytta            |");
            Console.WriteLine("| 5. Transaktioner     |");
            Console.WriteLine("| 6. Logga ut          |");
        }

        internal static void LogOutProgress()
        {
            Console.WriteLine("Thank you for using the ATM app");
            Utility.PrintDotAnimation();
            Console.Clear();
        }
    }
}
