using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankSystem
{
    class Delete
    {
        int CursorPostitionX1;
        int CursorPositionY1;
        String AccountNum;

        public void inputDelete()
        {
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\t\tDELETE");
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.Write("\n\t\t\t\tAccount Number: ");
            CursorPostitionX1 = Console.CursorLeft;
            CursorPositionY1 = Console.CursorTop;

            AccountNum = promptUser(CursorPostitionX1, CursorPositionY1);
            check(AccountNum);
            findAccount(AccountNum);

        }

        public void check(String AccountNum)
        {
            //Checks if account number is more than 10 characters long or if any is inputed at all
            if (AccountNum.Length > 10 || AccountNum.Length == 0)
            {
                //Error message of invalid account
                Console.WriteLine("\t\t\t\tInvalid account number");
                Console.ReadKey();
                Console.Clear();
                inputDelete();
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
                    inputDelete();
                    deleteAgain();
                }
            }
        }
        
        //Mehtod to find account
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
                deleteAccount();
            }
            else
            {
                //Account number not found message
                Console.WriteLine("\t\t\t\tAccount number " + AccountNum + " not found");
                Console.Write("\t\t\t\t---------------------------------------------");
            }
            deleteAgain();
        }

        //print the details of the account
        public String printDetails(String AccountNum)
        {
            string[] Accounts = File.ReadAllLines(AccountNum + ".txt");
            String details = "";
            //foreach loops iterates through each string in the Accounts array
            for (int loopVar = 0; loopVar < 6; loopVar++)
            {
                //prints out each string in Account array
                Console.WriteLine("\t\t\t\t" + Accounts[loopVar]);
                details += Accounts[loopVar] + "\n";
            }
            return details;
        }

        //Deletes fiel name accountNum.txt
        public void deleteAccount()
        {
            //asks for confirmation
            String response = "";
            Console.WriteLine("\t\t\t\tDelete this account? ('y' = yes/other key = no):\n");
            Console.Write("\t\t\t\t");
            int CursorPositionX3 = Console.CursorLeft;
            int CursorPositionY3 = Console.CursorTop;
            Console.SetCursorPosition(CursorPositionX3, CursorPositionY3);
            response = Console.ReadLine().ToLower();
            //If response is y than proceed to deleteing file
            if (response == "y")
            {
                File.Delete(AccountNum + ".txt");
                Console.WriteLine("\t\t\t\tAccount has sucesssfully been deleted");
            }
            //if other key is pressed than cancel to delete file
            else
            {
                Console.WriteLine("\t\t\t\tDelete process have been cancelled");
            }
        }

        //Prompts users to if they want to delete another account
        public void deleteAgain()
        {
            Console.Write("\n\t\t\t\tDelete another account? (press 'y'/other key = no):\n");
            String response = "";
            response = Console.ReadLine().ToLower();
            Console.Write("\t\t\t\t");
            int CursorPositionX3 = Console.CursorLeft;
            int CursorPositionY3 = Console.CursorTop;
            Console.SetCursorPosition(CursorPositionX3, CursorPositionY3);
            if (response == "y")
            {
                Console.Clear();
                inputDelete();
            }
            else
            {
                Console.ReadKey();
                Console.Clear();
            }
        }

        //prompts users for respond
        public String promptUser(int x, int y)
        {
            String input;
            Console.SetCursorPosition(x, y);
            input = Console.ReadLine();
            return input;
        }
    }
}
