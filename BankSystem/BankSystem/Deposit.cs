using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankSystem
{
    class Deposit
    {
        SearchForAccount s1 = new SearchForAccount();
        int CursorPostitionX1;
        int CursorPositionY1;
        int CursorPostitionX2;
        int CursorPositionY2;
        String AccountNum = "";
        double amount = 0;

        //Displays user interface and required fields to be filled
        public void inputDeposit()
        {
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\t\tDEPOSIT");
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.Write("\n\t\t\t\tAccount Number: ");
            CursorPostitionX1 = Console.CursorLeft;
            CursorPositionY1 = Console.CursorTop;
            //Asks user to input account number and stores it in variable
            AccountNum = promptUser(CursorPostitionX1, CursorPositionY1);
            //Checks if account number is valid
            check(AccountNum);
            findAccount(AccountNum);
        }

        //Check if account number is valid
        public void check(String AccountNum)
        {
            //Checks if account number is more than 10 characters long or if any is inputed at all
            if (AccountNum.Length > 10 || AccountNum.Length == 0)
            {
                //Error message of invalid account
                Console.WriteLine("\t\t\t\tInvalid account number");
                Console.ReadKey();
                Console.Clear();
                inputDeposit();
            }
            else
            {
                //Check if the input can be converted into an int (check if an int is entered)
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
                    inputDeposit();
                    depositAgain();
                }
            }
        }
        //Find account with account number input
        public void findAccount(String AccountNum)
        {
            //Search an account based on AccountNum with .txt, the naming convention of file
            if (File.Exists(AccountNum + ".txt"))
            {
                //If account was found display message and add amount
                try
                {
                    Console.WriteLine("\t\t\t\t---------------------------------------------");
                    //Account found message that account has been found
                    Console.WriteLine("\t\t\t\tAccount found, Please enter deposit amount");
                    Console.Write("\n\t\t\t\tAmount: $");
                    CursorPostitionX2 = Console.CursorLeft;
                    CursorPositionY2 = Console.CursorTop;
                    //Enter amount for deposit
                    amount = Convert.ToDouble(promptUser(CursorPostitionX2, CursorPositionY2));
                    //Updates balance and record transaction
                    addDeposit(amount);
                }
                //Cathc error and display errro message
                catch (Exception e)
                {
                    Console.WriteLine("\n\t\t\t\tInvalid Input");
                    Console.ReadKey();
                    Console.Clear();
                    inputDeposit();
                }
            }
            //If no file was found with inputed account number
            else
            {
                //Account number not found message
                Console.WriteLine("\t\t\t\tAccount number " + AccountNum + " not found");
                Console.ReadKey();
                depositAgain();
            }
        }

        //Adds deposti amount to balance and records transaction
        public void addDeposit(double amount)
        {
            try
            {
                //Reads the file with accounNumber.txt and stores it in account
                string account = File.ReadAllText(AccountNum + ".txt");
                //Split each information in accountNumber.txt everytime there is ':' and '\n' and stores each in array
                string[] accountDetails = account.Split(':', '\n');
                //gets array index number 11, where balance information is stored
                string intialBalance = accountDetails[11];
                //converts the balance from string to double
                double balance = Convert.ToDouble(intialBalance);
                //Checks if amount is <= 0 than users cannot deposit this amount
                if (amount <= 0)
                {
                    //Error message that amount is not valid
                    Console.WriteLine("\t\t\t\tYou cannot deposit this amount");
                }
                else
                {
                    //adds amount to the balance
                    balance += amount;
                    //converts the balance to string again
                    string accountbalance = Convert.ToString(balance);
                    //Opens the string account and replaces the intial balance with account balance (updates new balance)
                    account = account.Replace(intialBalance, accountbalance);
                    //Writes the update into the file
                    File.WriteAllText(AccountNum + ".txt", account);
                    //Records the transaction for A/C Statement
                    record(amount);
                    //Sucessful deposit message
                    Console.Write("\n\t\t\t\tDeposit Successful! Press Enter to Continue");
                }
                Console.ReadKey();
                //Asks if users want to deposit again
                depositAgain();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t\t\t\tPlease enter valid amount\n");
                Console.ReadKey();
                Console.Clear();
                inputDeposit();
            }
        }
        public void depositAgain()
        {
            Console.Write("\n\t\t\t\tMake another deposit? ('y' = yes/other key = no):\n");
            String response = "";
            Console.Write("\t\t\t\t");
            int CursorPositionX3 = Console.CursorLeft;
            int CursorPositionY3 = Console.CursorTop;
            Console.SetCursorPosition(CursorPositionX3, CursorPositionY3);
            //Prompts user and stores response in response variable
            response = Console.ReadLine().ToLower(); ;
            //if 'y' is entered than repeats the whole process again
            if (response == "y")
            {
                Console.Clear();
                inputDeposit();
            }
            //if other keys are entered than goes back to main menu
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

        //Records the transaction made in the account
        public void record(double transaction)
        {
            //Appends the transaction amount to the file for A/C Statement
            File.AppendAllText(AccountNum + ".txt", "Deposit($):" + transaction + "\n");
        }
    }
}
