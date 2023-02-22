using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankomatApp.UI
{
    public static class Utility
    {
        public static string GetSecretInput(string prompt)
        {
            bool isPrompt = true;
            string asterics = "";

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
                        PrintMessage("\nPlease enter 4 digits", "red");
                        input.Clear();
                        isPrompt = true;
                        continue;
                    }
                }

                if (inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                }
                else if (inputKey.Key != ConsoleKey.Backspace) 
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(asterics + "*");
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
                    // Used when input is correct
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "red":
                    // Used when input is wrong
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    // code block
                    break;
            }



            // Print message
            Console.WriteLine(msg);

            // Restore color
            Console.ResetColor();
            PressEnterToContinue();
        }

        public static string GetUserInput(string prompt)
        {
            Console.WriteLine($"Enter {prompt}");
            return Console.ReadLine();
        }

        public static void PrintDotAnimation(int timer = 25)
        {
            Console.Write("\nLoading");
            for (int i = 0; i < timer; i++)
            {
                Console.Write(".");
                Thread.Sleep(20);
            }
            Console.Clear();
        }

        public static void PressEnterToContinue()
        {
            Console.WriteLine("\n\nTryck retur för att fortsätta...\n");
            Console.ReadLine();
        }
    }
}
