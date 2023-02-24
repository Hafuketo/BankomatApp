using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatApp.Domain.Enums
{
    internal enum AppMenu
    {
        CheckBalance = 1,
        PlaceDeposit,
        MakeWithdrawal,
        InternalTransfer,
        ViewTransactions,
        Logout
    }
}
