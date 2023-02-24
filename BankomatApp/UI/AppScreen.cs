using BankomatApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BankomatApp.UI
{
    public class AppScreen
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
            Console.WriteLine("Tack för att du använder bankomatappen.");
            Utility.PrintDotAnimation();
            Console.Clear();
        }

        internal static int SelectAmount()
        {
            Console.WriteLine("");
            Console.WriteLine(":1.100 kr     5.1000 kr");
            Console.WriteLine(":2.200 kr     6.1500 kr");
            Console.WriteLine(":3.400 kr     7.2000 kr");
            Console.WriteLine(":4.500 kr     8.5000 kr");
            Console.WriteLine(":0.Övrigt belopp");
            Console.WriteLine("");

            int selectedAmount = Validator.Convert<int>("option: ");
            switch(selectedAmount)
            {
                case 1:
                    return 100;
                    break;
                case 2:
                    return 200;
                    break;
                case 3:
                    return 400;
                    break;
                case 4:
                    return 500;
                    break;
                case 5:
                    return 1000;
                    break;
                case 6:
                    return 1500;
                    break;
                case 7:
                    return 2000;
                    break;
                case 8:
                    return 5000;
                    break;
                case 0:
                    return 0;
                    break;
                default:
                    Utility.PrintMessage("Ogiltigt val. Försök igen.", "red");
                    return -1;
                    break;
            }
        }

        internal InternalTransfer InternalTransferForm()
        {
            var internalTransfer = new InternalTransfer();
            internalTransfer.ReciepeintBankAccountNumber = Validator.Convert<long>("Mottagares kontonummer: ");
            internalTransfer.TransferAmount = Validator.Convert<decimal>("mängd ");
            internalTransfer.ReciepeintBankAccountName = Utility.GetUserInput("Mottagares namn: ");
            return internalTransfer;

        }
    }
}
