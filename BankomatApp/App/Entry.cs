using BankomatApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatApp.App
{
    internal class Entry
    {
        static void Main(string[] args)
        {
            AppScreen.Welcome();
            
            //long cardNumber = Validator.Convert<long>("Your card num");

            //Console.WriteLine($"Your card number is {cardNumber}");

            BankomatApp bankomatApp = new BankomatApp();
            bankomatApp.InitializeData();
            bankomatApp.CheckUserCardNumAndPassword();
            bankomatApp.Welcome();
        }
    }
}
