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
    public class BankomatApp : IUserLogin, IUserAccountActions
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;
        private List<Domain.Entities.Transaction> _listOfTransactions;

        // Automatically runs when app is opened
        public void Run()
        {
            AppScreen.Welcome();
            CheckUserCardNumAndPassword();
            AppScreen.WelcomeCustomer(selectedAccount.FullName);
            while(true)
            {
                AppScreen.DisplayAppMenu(selectedAccount.FullName);
                ProccessMenuOption();
            }
        }

        // Creates a list of users with accounts and pin codes
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

        // Login checker for card number + PIN
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
                                break;
                            }
                        }
                    }
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

        // Checks which option was chosen
        private void ProccessMenuOption()
        {
            switch(Validator.Convert<int>("Input:"))
            {
                case 1:
                    CheckBalance();
                    break;
                case 2:
                    MakeWithdrawal();
                    break;
                case 3:
                    ViewTransaction();
                    break;
                case 4:
                    PlaceDeposit();
                    break;
                case 5:
                    AppScreen.LogOutProgress();
                    Utility.PrintMessage(
                     "\n           Du är nu utloggad.\n\r" +
                      $"           Vänligen ta ditt kort.\n\r" +
                       "   /\\___/\\  /\n\r" +
                       "  ( >^,^< )\n\r" +
                       "   /     \\__\n\r" +
                       "   \\(_|_)__ \\\n\r" +
                       "        (___/ ", "green");

                    Utility.PressEnterToContinue();
                    Run();
                    break;
                default:
                    Utility.PrintMessage("Ogiltigt val.", "red");
                    ProccessMenuOption();
                    break;
            }
        }

        // See how much money is on the account
        public void CheckBalance()
        {
            Console.Clear();

            Utility.PrintMessage(
            "#------ Bankomaten ------#\n" +
            "|                        |\n" +
            "| 1. Kortets saldo       |\n" +
            "|                        |\n" +
            "#------------------------#\n", "cyan");
            Console.Write("Tillgängligt på kontot: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Utility.FormatAmount(selectedAccount.AccountBalance)}");
            Console.ForegroundColor = ConsoleColor.White;
            Utility.PressEnterToContinue();
        }

        // Withdraw money from account 
        public void MakeWithdrawal()
        {
            var transaction_amt = 0;
            int selectedAmount = AppScreen.SelectAmount();

            if(selectedAmount == -1)
            {
                // Invalid choice
                MakeWithdrawal();
                return;
            }
            else if (selectedAmount != 0)
            {
                // Preset amount
                transaction_amt = selectedAmount;
            }
            else
            {
                // Your own amount
                Console.Clear();
                transaction_amt = Validator.Convert<int>($"Välj belopp: ");
            }

            // Make sure withdrawal isn't 0 or negative
            if(transaction_amt <= 0)
            {
                Utility.PrintMessage("Uttaget måste vara mer än 0. Försök igen.", "red");
                Utility.PressEnterToContinue();
                return;
            }
            // Make sure withdrawal isn't above 20 000 kr
            if (transaction_amt > 20000)
            {
                Utility.PrintMessage("Utag kan göras på max 20 000 kronor.", "red");
                Utility.PrintMessage("\nOm du vill ta ut mer kan du kontakta ditt lokala kontor.", "yellow");
                Utility.PressEnterToContinue();
                return;
            }

            // Make sure withdrawal ends with 50 or 00
            if (transaction_amt % 50 != 0)
            {
                Utility.PrintMessage("Uttag görs med 50, 100 och 500-kronorssedlar. Vänligen försök igen.", "red");
                Utility.PressEnterToContinue();
                return;
            }
            // Check if there is enough money to make withdrawal
            if (transaction_amt > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage($"Uttag misslyckades. Du har för lite pengar för att ta ut {Utility.FormatAmount(transaction_amt)}", "red");
                Utility.PressEnterToContinue();
                return;
            }

            // Bind withdrawal details to transaction object
            InsertTransaction(selectedAccount.Id, TransactionType.Withdrawal, -transaction_amt, "");

            // Update account balance
            selectedAccount.AccountBalance -= transaction_amt;

            if (PreviewBankNotesCount(transaction_amt) == false)
            {
                Utility.PrintMessage($"Du har avbrutit.", "red");
                Utility.PressEnterToContinue();
                return;
            }

            // Simulate counting
            Console.Clear();
            Console.WriteLine("\nKontrollerar uttag och räknar sedlar.");
            Utility.LoadingAnimation();
            Console.WriteLine("");

            // Sucess message
            Utility.PrintMessage($"Du har tagit ut " +
                 $"{Utility.FormatAmount(transaction_amt)}.", "green");
            Utility.PressEnterToContinue();

        }

        // Shows how many bank notes will be withdrawn
        private bool PreviewBankNotesCount(int amount)
        {
            int fiveHundredNotesCount = amount / 500;
            int oneHundredNotesCount = (amount % 500) / 100;
            int fiftyNotesCount = (amount % 100) / 50;

            Console.Clear();

            Utility.PrintMessage(
             "#----- 2. Uttag -----#\n" +
             "|                    |\n" +
             "| Summering av uttag |\n" +
             "|                    |\n" + 
             "#--------------------#\n", "cyan");

            Console.WriteLine($"500 x {fiveHundredNotesCount} = {500 * fiveHundredNotesCount}");
            Console.WriteLine($"100 x {oneHundredNotesCount} = {100 * oneHundredNotesCount}");
            Console.WriteLine($"50 x {fiftyNotesCount} = {50 * fiftyNotesCount}");
            Console.WriteLine($"\nTotalt belopp: {Utility.FormatAmount(amount)}\n\n");

            Utility.PrintMessage("1. Godkänn", "green");
            Utility.PrintMessage("0. Avbryt", "red");

            int opt = Validator.Convert<int>("");
            return opt.Equals(1);
        }

        // Choosing which account for inserting
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

        // See history of transactions
        public void ViewTransaction()
        {
            var filteredTransactionList = _listOfTransactions.Where(t => t.UserBankAccountId == selectedAccount.Id).ToList();
            // Check if there are any transactions
            if(filteredTransactionList.Count <= 0 )
            {
                Utility.PrintMessage("You have no transactions yet", "yellow");
            }
            else
            {
                Utility.PrintMessage("You have some transactions", "green");
            }
        }


        // Deposit money to account
        public void PlaceDeposit()
        {
            Console.Clear();
            Utility.PrintMessage(
            "#------ Bankomaten ------#\n" +
            "|                        |\n" +
            "| 4. Kontantinsättning   |\n" +
            "|                        |\n" +
            "#------------------------#\n", "cyan");
            Utility.PrintMessage("2. Insättning av kontanter\n", "cyan");
            Console.WriteLine("\nHur mycket vill du sätta in? (Lägsta summa 100 kronor).\n");
            var transaction_amt = Validator.Convert<int>($"Total summa: ");

            // Simulate counting
            Console.Clear();
            Console.WriteLine("\nKontrollerar och räknar sedlar.");
            Utility.LoadingAnimation();
            Console.WriteLine("");

            // Some guard clause
            if (transaction_amt < 1)
            {
                Utility.PrintMessage("Beloppet måste vara minst 1 krona, försök igen.", "red");
                Utility.PressEnterToContinue();
                PlaceDeposit();
            }

            if (transaction_amt > 10000)
            {
                Utility.PrintMessage("Insättningsgränd 10 000 kronor.", "red");
                Utility.PrintMessage("\n För att sätta in mer kan du kontakta ditt lokala kontor.", "yellow");
                Utility.PressEnterToContinue();
                PlaceDeposit();
            }

            // Bind transaction detalis to transaction object
            InsertTransaction(selectedAccount.Id, TransactionType.Deposit, transaction_amt, "");

            // Update account balance
            selectedAccount.AccountBalance += transaction_amt;

            // Print sucessmsg
            Utility.PrintMessage($"Du har gjort en insättning på {Utility.FormatAmount(transaction_amt)}.", "green");
            Utility.PressEnterToContinue();

        }

    }
}