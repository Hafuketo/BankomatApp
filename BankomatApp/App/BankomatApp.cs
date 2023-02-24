using BankomatApp.Domain.Entities;
using BankomatApp.Domain.Enums;
using BankomatApp.Domain.Interfaces;
using BankomatApp.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Transactions;

namespace BankomatApp
{
    public class BankomatApp : IUserLogin, IUserAccountActions, ITransaction
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;
        private List<Domain.Entities.Transaction> _listOfTransactions;
        private const decimal minimumKeptAmount = 100;

        public void Run()
        {
            AppScreen.Welcome();
            CheckUserCardNumAndPassword();
            AppScreen.WelcomeCustomer(selectedAccount.FullName);
            AppScreen.DisplayAppMenu();
            ProccessMenuOption();
        }
        public void InitializeData()
        {
            userAccountList = new List<UserAccount>
            {
                new UserAccount{Id=1, FullName = "Uffe", AccountNumber=123456,CardNumber =1, CardPin=1,AccountBalance=50000.00m,IsLocked=false},
                new UserAccount{Id=2, FullName = "Bullen", AccountNumber=456789,CardNumber =2, CardPin=1,AccountBalance=4000.00m,IsLocked=false},
                new UserAccount{Id=3, FullName = "Minka", AccountNumber=123555,CardNumber =3, CardPin=1,AccountBalance=2000.00m,IsLocked=false},
            };
            _listOfTransactions = new List<Domain.Entities.Transaction>();
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

        private void ProccessMenuOption()
        {
            switch(Validator.Convert<int>("an option:"))
            {
                case (int)AppMenu.CheckBalance:
                    //Console.WriteLine("Kontrollerar kontobalans...");
                    CheckBalance();
                    break;
                case (int)AppMenu.PlaceDeposit:
                    //Console.WriteLine("Placerar insättning...");
                    PlaceDeposit();
                    break;
                case (int)AppMenu.MakeWithdrawal:
                    //Console.WriteLine("Gör uttag...");
                    MakeWithdrawal();
                    break;
                case (int)AppMenu.InternalTransfer:
                    Console.WriteLine("Flytta mellan konton...");
                    //InternalTransfer();
                    break;
                case (int)AppMenu.ViewTransactions:
                    Console.WriteLine("Ser transaktionshistorik...");
                    break;
                case (int)AppMenu.Logout:
                    AppScreen.LogOutProgress();
                    Utility.PrintMessage("Du är nu utloggad.\n\nVänligen ta ut ditt kort.", "green");
                    Run();
                    break;
                default:
                    Utility.PrintMessage("Invalid option.", "red");
                    ProccessMenuOption();
                    break;
            }
        }

        public void CheckBalance()
        {
            Utility.PrintMessage($"Your account balance is: {Utility.FormatAmount(selectedAccount.AccountBalance)}", "green");
        }

        public void PlaceDeposit()
        {
            Console.WriteLine("\nEndast multiplicerbara med 100 kr tillåtet.\n");
            var transaction_amt = Validator.Convert<int>($"Total summa: ");

            // Simulate counting
            Console.WriteLine("\nKontrollerar och räknar sedlar.");
            Utility.PrintDotAnimation();
            Console.WriteLine("");

            // Some guard clause
            if (transaction_amt < 100) {
                Utility.PrintMessage("Beloppet måste vara minst 100, försök igen.","red");
                return;
            }
            if (transaction_amt % 100 != 0)
            {
                Utility.PrintMessage($"Beloppet måste vara multiplicerbart med 100. Försök igen.", "red");
                return;
            }

            if(PreviewBankNotesCount(transaction_amt) == false)
            {
                Utility.PrintMessage($"Du har avbrutit.", "red");
                return;
            }

            // Bind transaction detalis to transaction object
            InsertTransaction(selectedAccount.Id, TransactionType.Deposit, transaction_amt, "");

            // Update account balance
            selectedAccount.AccountBalance += transaction_amt;

            // Print sucessmsg
            Utility.PrintMessage($"Du har gjort en insättning på {Utility.FormatAmount(transaction_amt)}.", "green");
     
        }

        public void MakeWithdrawal()
        {
            var transaction_amt = 0;
            int selectedAmount = AppScreen.SelectAmount();
            if(selectedAmount == -1)
            {
                selectedAmount = AppScreen.SelectAmount();
            } else if (selectedAmount != 0)
            {
                transaction_amt = selectedAmount;
            } else
            {
                transaction_amt = Validator.Convert<int>($"amount ");
            }

            // input validation
            if(transaction_amt <= 0)
            {
                Utility.PrintMessage("Amount needs to be greater than 0. try again.", "red");
                return;
            }
            if(transaction_amt % 100 != 0)
            {
                Utility.PrintMessage("You can only withdraw amount in multiples of 100. Try again", "red");
                return;
            }

            //Business lfc validations
            if (transaction_amt > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage($"Withdrawal failed. Your balance is too low to withdraw" +
                    $"{Utility.FormatAmount(transaction_amt)}", "red");
                return;
            }
            if((selectedAccount.AccountBalance - transaction_amt) < minimumKeptAmount)
            {
                Utility.PrintMessage($"Uttag misslyckades. Ditt kontos saldå är för lågt. Du måste ha minst {Utility.FormatAmount(minimumKeptAmount)} tillgängligt" , "red");
            }
            // Bind withdrawal details to transaction object
            InsertTransaction(selectedAccount.Id, TransactionType.Withdrawal, -transaction_amt, "");

            // Update account balance
            selectedAccount.AccountBalance -= transaction_amt;

            // Sucess message
            Utility.PrintMessage($"You have sucessfully withdrawn {Utility.FormatAmount(transaction_amt)}.", "red");

        }

        private bool PreviewBankNotesCount(int amount)
        {
            int fiveHundredNotesCount = amount / 500;
            int oneHundredNotesCount = (amount % 500) / 100;

            Console.WriteLine("\nSummary");
            Console.WriteLine("------");
            Console.WriteLine($"500 x {fiveHundredNotesCount} ) = {500 * fiveHundredNotesCount}");
            Console.WriteLine($"100 x {oneHundredNotesCount} ) = {100 * oneHundredNotesCount}");
            Console.WriteLine($"Total amount: {Utility.FormatAmount(amount)}\n\n");

            int opt = Validator.Convert<int>("1 to confirm");
            return opt.Equals(1);
        }

        public void InsertTransaction(long _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc)
        {
            // create a new transaction object
            var transaction = new Domain.Entities.Transaction()
            {
                TransactionId = Utility.GetTransactionId(),
                UserBankAccountId = _UserBankAccountId,
                TransactionDate = DateTime.Now,
                TransactionType = _tranType,
                TransactionAmount = _tranAmount,
                Description = _desc
            };

            // Add transaction object to the list
            _listOfTransactions.Add(transaction);
        }

        public void ViewTransaction()
        {
            throw new NotImplementedException();
        }
    }
}