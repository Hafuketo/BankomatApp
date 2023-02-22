using BankomatApp.Domain.Entities;
using BankomatApp.Domain.Interfaces;
using BankomatApp.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BankomatApp
{
    public class BankomatApp : IUserLogin
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;

        public void InitializeData()
        {
            userAccountList = new List<UserAccount>
            {
                new UserAccount{Id=1, FullName = "Uffe", AccountNumber=123456,CardNumber =1, CardPin=1234,AccountBalance=50000.00m,IsLocked=false},
                new UserAccount{Id=2, FullName = "Bullen", AccountNumber=456789,CardNumber =2, CardPin=1234,AccountBalance=4000.00m,IsLocked=false},
                new UserAccount{Id=3, FullName = "Minka", AccountNumber=123555,CardNumber =3, CardPin=1234,AccountBalance=2000.00m,IsLocked=false},
            };
        }

        public void CheckUserCardNumAndPassword()
        {
            bool isCorrectLogin = false;
            while (isCorrectLogin == false)
            {
                UserAccount inputAccount = AppScreen.UserLoginForm();
                AppScreen.LoginProgress();
                foreach (UserAccount account in userAccountList)
                {
                    selectedAccount = account;
                    if (inputAccount.CardNumber.Equals(selectedAccount.CardNumber))
                    {
                        selectedAccount.TotalLogin++;

                        if (inputAccount.CardPin.Equals(selectedAccount.CardPin))
                        {
                            selectedAccount = account;

                            if (selectedAccount.IsLocked || selectedAccount.TotalLogin > 3)
                            {
                                // Print lock message
                                AppScreen.PrintLockScreen();
                            }
                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isCorrectLogin = true; 
                                Console.WriteLine("0: " + isCorrectLogin);
                                break;
                            }
                        }
                    }
                    Console.WriteLine("1: " + isCorrectLogin);
                    if (isCorrectLogin == false)
                    {
                        Console.WriteLine($"test: {selectedAccount.FullName}");
                        Console.WriteLine("2: " + isCorrectLogin);
                        Utility.PrintMessage("\nFelaktigt kortnummer eller PIN.", "red");
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
            Console.WriteLine($"Välkommen tillbaka, {selectedAccount.FullName}");
            Utility.PressEnterToContinue();
        }


    }
}