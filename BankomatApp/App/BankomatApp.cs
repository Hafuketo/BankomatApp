using BankomatApp.UI;
using System;

namespace BankomatApp.App
{
    class BankomatApp
    {
        static void Main(string[] args)
        {
            AppScreen.Welcome();
            // string cardNumber = Utility.GetUserInput("your card numer");
            long cardNumber = Validator.Convert<long>("Your card num");

            Console.WriteLine($"Your card number is {cardNumber}");

            Utility.PressEnterToContinue();

        }
    }
}