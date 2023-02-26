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
            Console.WriteLine("");
            Utility.PrintMessage("#==============================================================#" +
                               "\n|----------------Välkommen till Neko's bankomat----------------|\n" +
                                 "#==============================================================#\n", "cyan");

            Utility.PrintMessage(
                "                         ____.-´`-.____\r\n                   _____[.-´________`-.]_____\r\n                  [__________ BANK __________]\r\n                   [________________________]\r\n                     ||==||==||==||==||==||\r\n                     ||==||==||==||==||==||\r\n                     ||==||==||==||==||==||\r\n                     ||==||==||  ||==||==||\r\n                     ||==||==||  ||==||==||\r\n                    /======================\\\r\n                   /========================\\\n", "cyan");

            // Prompt user to insert card
            Console.WriteLine("Vänligen sätt in ditt bankkort i bankomaten");
            Utility.PrintMessage("(Obs!; En riktig automat skulle här begära och läsa av ett fysiskt kort)", "yellow");
            Utility.PressEnterToContinue();
        }

        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<long>("Kortnummer: ");
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("PIN-kod: "));
            return tempUserAccount;
        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\nKontrollerar kortnummer och PIN...");
            Utility.PrintDotAnimation();
        }

        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Ditt konto har låsts. Vänligen kontakta närmaste bankkontor för att få hjälp.", "yellow");
            Environment.Exit(1);
        }

        internal static void WelcomeCustomer(string fullName)
        {
            Console.WriteLine($"Välkommen {fullName}");
            Utility.PressEnterToContinue();
        }

        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Utility.PrintMessage(
            "#------ Bankomaten ------#\n" +
            "|                        |\n" +
            "| Vad vill du göra idag? |\n" +
            "|                        |\n" +
            "| 1. Saldo               |\n" +
            "| 2. Uttag               |\n" +
            "| 3. Historik            |\n" +
            "| 4. Insättning          |\n" +
            "| 5. Logga ut            |\n" +
            "|                        |\n" +
            "#------------------------#\n", "cyan");
        }

        internal static void LogOutProgress()
        {
            Console.WriteLine("Tack för att du använt denna bankomat.");
            Console.WriteLine("Välkommen åter.");
            Utility.PrintDotAnimation();
            Console.Clear();
        }

        internal static int SelectAmount()
        {
            Console.WriteLine("\nHur mycket vill du ta ut?\n");
            Console.WriteLine(":1.100 kr     5.1000 kr");
            Console.WriteLine(":2.200 kr     6.1500 kr");
            Console.WriteLine(":3.400 kr     7.2000 kr");
            Console.WriteLine(":4.500 kr     8.5000 kr");
            Console.WriteLine(":0.Övrigt belopp");
            Console.WriteLine("");

            int selectedAmount = Validator.Convert<int>("Val: ");
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

    }
}
