using BankomatApp.Domain.Entities;
using BankomatApp.Domain.Interfaces;
using BankomatApp.UI;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace BankomatApp.App
{
    public class BankomatApp : IUserLogin
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;

        public void InitializeData()
        {
            userAccountList = new List<UserAccount>
            {
                new UserAccount { Id = 1, FullName = "Uffe", AccountNumber = 123123, CardNumber = 321321, CardPin = 121212, AcoountBalance = 50000.00m, IsLocked = false },
                new UserAccount { Id = 2, FullName = "Effi", AccountNumber = 123124, CardNumber = 321322, CardPin = 212121, AcoountBalance = 70000.00m, IsLocked = false },
                new UserAccount { Id = 3, FullName = "Debb", AccountNumber = 123125, CardNumber = 321323, CardPin = 122112, AcoountBalance = 20000.00m, IsLocked = false },
            };
        }

        public void CheckUserCardNumAndPassword()
        {
            bool isCorrectLogin = false;
            while(isCorrectLogin == false)
            {
                UserAccount inputAccount = AppScreen.UserLoginForm();
                AppScreen.LoginProgress();
                foreach(UserAccount account in userAccountList)
                {
                    selectedAccount = account;
                    if(inputAccount.CardNumber.Equals(selectedAccount.CardNumber))
                    {
                        selectedAccount.TotalLogin++;

                        if(inputAccount.CardPin.Equals(selectedAccount.CardPin ))
                        {
                            selectedAccount = account;

                            if(selectedAccount.IsLocked || selectedAccount.TotalLogin < 3)
                            {
                                // Print lock message
                                AppScreen.PrintLockScreen();
                            } 
                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isCorrectLogin = true;
                                break;
                            }
                        }
                    }
                    if (isCorrectLogin == false)
                    {
                        Utility.PrintMessage("\n Invalid card number or PIN", false);
                        selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                        if (selectedAccount.IsLocked)
                        {
                            AppScreen.PrintLockScreen();
                        }
                    }
                    Console.Clear();
                }
            }
        }
        public void Welcome()
        {
            Console.WriteLine($"Welcome back, {selectedAccount.FullName}");
        }
        
    }
}