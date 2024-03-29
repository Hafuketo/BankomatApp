﻿using BankomatApp.Domain.Entities;
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
                "                         ____.-'`-.____\r\n                   _____[.-'________`-.]_____\r\n                  [__________ BANK __________]\r\n                   [________________________]\r\n                     ||==||==||==||==||==||\r\n                     ||==||==||==||==||==||\r\n                     ||==||==||==||==||==||\r\n                     ||==||==||  ||==||==||\r\n                     ||==||==||  ||==||==||     /\\__/\\\r\n                    /======================\\   ( ^,^  )_)\r\n                   /========================\\   (u  u   )", "cyan");


            
            // Prompt user to insert card
            Console.WriteLine("Vänligen sätt in ditt bankkort i bankomaten\n");
            Utility.PrintMessage(
                "(Obs!; En riktig automat skulle här begära \n" +
                "och läsa av ett fysiskt kort. Istället \n" +
                "kommer du att fylla i ett kortnummer.)\n", "yellow");
        }

        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<long>("Kortnummer: ");
            Console.WriteLine("");
            string pin = Utility.GetSecretInput("PIN-kod: ");

            bool valid = false;
            while (!valid)
            {
                try
                {
                    tempUserAccount.CardPin = Convert.ToInt32(pin);
                    valid = true;
                }
                catch
                {
                    Utility.PrintMessage("\nOgiltigt val. Försök igen.", "red");
                    pin = Utility.GetSecretInput("PIN-kod: ");
                }
                
            }

            Console.WriteLine("");
            return tempUserAccount;
        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\nKontrollerar kortnummer och PIN...");
            Utility.LoadingAnimation();
        }

        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Ditt konto har låsts. Vänligen kontakta närmaste bankkontor för att få hjälp.", "yellow");
            Utility.PressEnterToContinue();
        }

        internal static void WelcomeCustomer(string fullName)
        {
            Utility.PrintMessage(
            "\n           Välkommen\n\r" +
           $"           {fullName}\n\r" +
           "   /\\___/\\  /\n\r" +
           "  ( >^,^< )\n\r" +
           "   /     \\__\n\r" +
           "   \\(_|_)__ \\\n\r" +
           "        (___/ ", "cyan");

            Utility.PressEnterToContinue();
        }

        internal static void DisplayAppMenu(string fullName)
        {
            Console.Clear();
            Utility.PrintMessage($"Användare: {fullName}", "green");
            Utility.PrintMessage(
            "#------ Bankomaten ------#\n" +
            "|                        |\n" +
            "| Vad vill du göra idag? |\n" +
            "|                        |\n" +
            "| 1. Saldo               |\n" +
            "| 2. Uttag               |\n" +
            "| 3. Insättning          |\n" +
            "| 4. Logga ut            |\n" +
            "|                        |\n" +
            "#------------------------#\n", "cyan");
        }

        internal static void LogOutProgress()
        {
            Console.Clear();
            Utility.PrintMessage(
          "\n           Tack för att du använt denna bankomat.\n\r" +
           $"           Välkommen åter.\n\r" +
           "   /\\___/\\  /\n\r" +
           "  ( >^,^< )\n\r" +
           "   /     \\__\n\r" +
           "   \\(_|_)__ \\\n\r" +
           "        (___/ ", "cyan");
            Utility.LoadingAnimation();
            Console.Clear();
            Utility.PrintMessage(
                     "\n           Du är nu utloggad.\n\r" +
                      $"           Vänligen ta ditt kort.\n\r" +
                       "   /\\___/\\  /\n\r" +
                       "  ( >^,^< )\n\r" +
                       "   /     \\__\n\r" +
                       "   \\(_|_)__ \\\n\r" +
                       "        (___/ ", "green");

            Utility.PressEnterToContinue();
        }

        internal static int SelectAmount()
        {
            Console.Clear();
            Utility.PrintMessage(
            "#--------- 2. Uttag ---------#\n" +
            "|                            |\n" +
            "| Hur mycket vill du ta ut?  |\n" +
            "|                            |\n" +
            "| 1. 100 kr      5. 1000 kr  |\n" +
            "| 2. 200 kr      6. 1500 kr  |\n" +
            "| 3. 400 kr      7. 2000 kr  |\n" +
            "| 4. 500 kr      8. 5000 kr  |\n" +
            "| 0. Övrigt belopp           |\n" +
            "|                            |\n" +
            "#----------------------------#\n", "cyan");

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
