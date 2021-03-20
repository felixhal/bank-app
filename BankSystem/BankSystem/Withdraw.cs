using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankSystem
{
    class Withdraw
    {
        int CursorPostitionX1;
        int CursorPositionY1;
        int CursorPostitionX2;
        int CursorPositionY2;
        String AccountNum = "";
        double amount = 0;

        //Display Withdraw fields
        public void inputWithdraw()
        {
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\t\tWITHDRAW");
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.Write("\n\t\t\t\tAccount Number: ");
            CursorPostitionX1 = Console.CursorLeft;
            CursorPositionY1 = Console.CursorTop;

            AccountNum = promptUser(CursorPostitionX1, CursorPositionY1);
            check(AccountNum);
            findAccount(AccountNum);
        }

        //checks if the account number is valid
        public void check(String AccountNum)
        {
            //Checks if account number is more than 10 characters long or if any is inputed at all
            if (AccountNum.Length > 10 || AccountNum.Length == 0)
            {
                //Error message of invalid account
                Console.WriteLine("\t\t\t\tInvalid account number");
                Console.ReadKey();
                Console.Clear();
                inputWithdraw();
            }
            else
            {
                //Check if the input can be converted into an int
                try
                {
                    Convert.ToInt32(AccountNum);
                }
                catch (Exception e)
                {
                    //Error message tha account number is invalid
                    Console.WriteLine("\t\t\t\tInvalid account number");
                    Console.ReadKey();
                    Console.Clear();
                    inputWithdraw();
                    withdrawAgain();
                }
            }
        }

        //finds the account with the certain student number
        public void findAccount(String AccountNum)
        {
            //Search an account based on AccountNum with .txt, the naming convention of file
            if (File.Exists(AccountNum + ".txt"))
            {
                try
                {
                    Console.WriteLine("\t\t\t\t---------------------------------------------");
                    //Account found message that account has been found
                    Console.WriteLine("\t\t\t\tAccount found, Please enter withdraw amount");
                    Console.Write("\n\t\t\t\tAmount: $");
                    CursorPostitionX2 = Console.CursorLeft;
                    CursorPositionY2 = Console.CursorTop;
                    amount = Convert.ToDouble(promptUser(CursorPostitionX2, CursorPositionY2));
                    minusWithdraw(amount);
                }
                catch (Exception e)
                {
                    Console.Write("\n\t\t\t\tInvalid Input");
                    Console.ReadKey();
                }
            }
            else
            {
                //Account number not found message
                Console.WriteLine("\t\t\t\tAccount number " + AccountNum + " not found");
                Console.ReadKey();
                withdrawAgain();
            }
        }

        //minuese withdraw amount with the balance
        public void minusWithdraw(double amount)
        {
            try
            {
                string account = File.ReadAllText(AccountNum + ".txt");
                string[] accountDetails = account.Split(':', '\n');
                string intialBalance = accountDetails[11];
                double balance = Convert.ToDouble(intialBalance);
                //If the balance is less thatn the amount than show message
                if (balance < amount)
                {
                    Console.Write("\n\t\t\t\tInsufficient funds in your account");
                }
                //if the withdraw amount is less than 0 and do not withdraw at all and do not record transaction
                else if (amount <= 0)
                {
                    Console.Write("\n\t\t\t\tPlease enter a valid amount to withdraw");
                }
                else
                {
                    balance -= amount;
                    string accountbalance = Convert.ToString(balance);
                    account = account.Replace(intialBalance, accountbalance);
                    File.WriteAllText(AccountNum + ".txt", account);
                    record(amount);
                    Console.Write("\n\t\t\t\tWithdraw Successful! Press Enter to Continue");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t\t\t\tPlease enter valid amount");
                Console.ReadKey();
            }
            withdrawAgain();
        }

        //Asks user if they want to withdraw again
        public void withdrawAgain()
        {
            Console.WriteLine("\n\t\t\t\tMake another withdraw? ('y' = yes/other key = no):\n");
            String response = "";
            Console.Write("\t\t\t\t");
            int CursorPositionX3 = Console.CursorLeft;
            int CursorPositionY3 = Console.CursorTop;
            Console.SetCursorPosition(CursorPositionX3, CursorPositionY3);
            response = Console.ReadLine().ToLower(); ;
            //if 
            if (response == "y")
            {
                Console.Clear();
                inputWithdraw();
            }
            else
            {
                Console.Clear();
            }
        }
        //prompts users for input based on the recorder cursor position
        public String promptUser(int x, int y)
        {
            String input;
            Console.SetCursorPosition(x, y);
            input = Console.ReadLine();
            return input;
        }

        public void record(double transaction)
        {
            File.AppendAllText(AccountNum + ".txt", "withdraw($): " + transaction + "\n");
        }
    }
}
