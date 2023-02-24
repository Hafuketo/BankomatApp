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
            BankomatApp bankomatApp = new BankomatApp();
            bankomatApp.InitializeData();
            bankomatApp.Run();

        }
    }
}
