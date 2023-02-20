using BankomatApp.Domain.Entities;
using BankomatApp.UI;
using System;
using System.Collections.Generic;

namespace BankomatApp.App
{
    public class BankomatApp
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;

        public void InitializeData()
        {
            userAccountList = new List<UserAccount>
            {
                new UserAccount { Id = 1, FullName = "Uffe", AccountNumber = 123123, CardNumber = 321321, CardPin = 1212, AcoountBalance = 50000.00m, IsLocked = false },
                new UserAccount { Id = 2, FullName = "Effi", AccountNumber = 123124, CardNumber = 321322, CardPin = 2121, AcoountBalance = 70000.00m, IsLocked = false },
                new UserAccount { Id = 3, FullName = "Debb", AccountNumber = 123125, CardNumber = 321323, CardPin = 1221, AcoountBalance = 20000.00m, IsLocked = false },
            };
        }
        
    }
}