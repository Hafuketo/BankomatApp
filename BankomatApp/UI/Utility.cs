﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankomatApp.UI
{
    public static class Utility
    {
        private static long tranId;
        private static CultureInfo culture = new CultureInfo("sv-se");

        public static long GetTransactionId()
        {
            return ++tranId;
        }
        public static string GetSecretInput(string prompt)
        {
            bool isPrompt = true;
            string passInput = "";

            StringBuilder input = new StringBuilder();

            while(true)
            {
                if(isPrompt)
                    Console.WriteLine(prompt);
                isPrompt = false;

                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    if(input.Length == 4)
                    {
                        break;
                    } 
                    else
                    {
                        PrintMessage("\nVänligen fyll i PIN (4 siffror)", "red");
                        input.Clear();
                        isPrompt = true;
                        continue;
                    }
                }

                if (inputKey.Key == ConsoleKey.Backspace)
                {
                    passInput = "";
                    input.Clear();
                    Console.WriteLine(passInput);
                }
                else
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(passInput + "*");
                }
            }
            return input.ToString();
        }

        //Prints message with specified color
        public static void PrintMessage(string msg, string color)
        {

            switch (color)
            {
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    // code block
                    break;
            }



            // Print message
            Console.WriteLine(msg);

            // Restore color
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static string GetUserInput(string prompt)
        {
            Console.WriteLine($"{prompt}");
            return Console.ReadLine();
        }

        public static void LoadingAnimation(int timer = 15)
        {

            Console.Write("\nVänta... ");
            for (int i = 0; i < timer; i++)
            {
                int waittime = 15;
                Console.CursorVisible = false;
                    Console.Write("/");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    System.Threading.Thread.Sleep(waittime);
                    Console.Write("-");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    System.Threading.Thread.Sleep(waittime);
                    Console.Write("\\");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    System.Threading.Thread.Sleep(waittime);
                    Console.Write("|");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    System.Threading.Thread.Sleep(waittime);
            }
            Console.CursorVisible = true;
            Console.Clear();
        }

        public static void PressEnterToContinue()
        {
            Console.WriteLine("\nTryck retur för att fortsätta...");
            Console.ReadLine();
        }
        
        public static string FormatAmount(decimal amt)
        {
            return String.Format(culture, "{0:C2}", amt);
        }
    }
}
