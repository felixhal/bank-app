using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankSystem
{
    class SearchForAccount
    {
        //Prompts user to input account number of account searched for
        public void inputSearch()
        {
            String AccountNum;
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\t\t\tSEARCH FOR ACCOUNT");
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.Write("\n\t\t\t\tPlease Enter Account Number: ");
            int CursorPostitionX1 = Console.CursorLeft;
            int CursorPositionY1 = Console.CursorTop;
            Console.SetCursorPosition(CursorPostitionX1, CursorPositionY1);
            //Prompts user input and stores it in AccountNum
            AccountNum = Console.ReadLine();
            check(AccountNum);
            findAccount(AccountNum);
        }

        //Checks if account number is valid
        public void check(String AccountNum)
        {
            //Checks if account number is more than 10 characters long or if any is inputed at all
            if (AccountNum.Length > 10 || AccountNum.Length == 0)
            {
                //Error message of invalid account
                Console.WriteLine("\t\t\t\tInvalid account number");
                Console.ReadKey();
                Console.Clear();
                inputSearch();
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
                    inputSearch();
                }
            }
        }

        //Search for a file and prints out details of the account
        public void findAccount(String AccountNum)
        {
            //Search an account based on AccountNum with .txt, the naming convention of file
            if (File.Exists(AccountNum + ".txt"))
            {
                //Account found message that account has been found
                Console.WriteLine("\t\t\t\tAccount number " + AccountNum + " found");
                Console.Write("\t\t\t\t---------------------------------------------\n");
                Console.Write("\n\t\t\t\tACCOUNT DETAILS:\n");
                printDetails(AccountNum);
            }
            else
            {
                //Account number not found message
                Console.WriteLine("\t\t\t\tAccount number " + AccountNum + " not found");
                Console.Write("\t\t\t\t---------------------------------------------");
            }
            Console.Write("\t\t\t\tSearch for another account? (y/n)\n");
            searchAgain();
        }

        public String printDetails(String AccountNum)
        {
            string[] Accounts = File.ReadAllLines(AccountNum + ".txt");
            String details = "";
            //foreach loops iterates through each string in the Accounts array
            for (int loopVar = 0; loopVar < 6; loopVar++)
            {
                //prints out each string in Account array
                Console.WriteLine("\t\t\t\t\t" + Accounts[loopVar]);
                details += Accounts[loopVar] + "\n";
            }
            return details;
        }

        //Prompts users to search another array or not?
        public void searchAgain()
        {
            String response = "";
            //Prompts user and stores response in response variable
            Console.Write("\t\t\t\t");
            int CursorPositionX2 = Console.CursorLeft;
            int CursorPositionY2 = Console.CursorTop;
            Console.SetCursorPosition(CursorPositionX2, CursorPositionY2);
            response = Console.ReadLine().ToLower();
            if (response == "y")
            {
                Console.Clear();
                inputSearch();
            }
            else if (response == "n")
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("\t\t\t\tInvalid response");
                Console.ReadKey();
            }
        }
    }
}
