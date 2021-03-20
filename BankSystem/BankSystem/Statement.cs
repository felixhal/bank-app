using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace BankSystem
{
    class Statement
    {
        String statement = "";
        //Prompts user to input account number of account searched for
        public void inputSearch()
        {
            String AccountNum;
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\t\t\tA/C STATEMENT");
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
                //searchAgain();
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
                Console.WriteLine("\t\t\t\t---------------------------------------------");
                Console.WriteLine("\n\t\t\t\tA/C STATEMENT: ");
                statement = printDetails(AccountNum) + printTransactions(AccountNum);
                emailOption();
            }
            else
            {
                //Account number not found message
                Console.WriteLine("\t\t\t\tAccount number " + AccountNum + " not found");
            }
            statement = "";
            searchAgain();
        }

        //
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

        //print the last 5 transaction list
        public String printTransactions(String AccountNum)
        {
            string[] Accounts = File.ReadAllLines(AccountNum + ".txt");
            String transations = "";
            int numOfTransactions = 5;
            //If the array length is more than 10
            if (Accounts.Length > 10)
            {
                //print the last 5 transactions only
                for (int loopVar = Accounts.Length - numOfTransactions; loopVar < Accounts.Length; loopVar++)
                {
                    Console.WriteLine("\t\t\t\t" + Accounts[loopVar]);
                    transations += Accounts[loopVar] + "\n";
                }
            }
            //if the array length is less than 10
            else
            {
                //print the all transactions
                for (int loopVar = 5; loopVar < Accounts.Length; loopVar++)
                {
                    Console.WriteLine("\t\t\t\t" + Accounts[loopVar]);
                    transations += Accounts[loopVar] + "\n";
                }
            }
            return transations;
        }

        //Prompts users to search another array or not?
        public void searchAgain()
        {
            Console.WriteLine("\t\t\t\tMake another statement? ('y' = yes/other key = no):\n");
            String response = "";
            Console.Write("\t\t\t\t");
            int CursorPositionX3 = Console.CursorLeft;
            int CursorPositionY3 = Console.CursorTop;
            Console.SetCursorPosition(CursorPositionX3, CursorPositionY3);
            response = Console.ReadLine().ToLower();
            if (response == "y")
            {
                Console.Clear();
                inputSearch();
            }
            else
            {
                Console.Clear();
            }
        }

        public void emailOption()
        {
            String response = "";
            //Prompts user and stores response in response variable
            Console.WriteLine("\n\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\tWould you like to email A/C Statement? (y = yess / other key = no)");
            int CursorPostitionX2 = Console.CursorLeft;
            int CursorPositionY2 = Console.CursorTop;
            Console.SetCursorPosition(CursorPostitionX2, CursorPositionY2);
            Console.Write("\n\t\t\t\t");
            response = Console.ReadLine().ToLower();

            if (response == "y")
            {
                emailStatement(statement);
            }
            else
            {
            }
        }

        //email statement
        public void emailStatement(String content)
        {
            try
            {
                //reads recipient email
                MailAddress to = new MailAddress(Console.ReadLine());
                //reads the sender email
                MailAddress from = new MailAddress("dummyfor.appdev.dotnet@gmail.com");

                MailMessage mail = new MailMessage(from, to);
                //Email subject
                mail.Subject = "A/C STATEMENT";
                //Email content parameter
                mail.Body = content;
                //smtp information
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;

                //login to email address (credentials)
                smtp.Credentials = new NetworkCredential("dummyfor.appdev.dotnet@gmail.com", "ut$!333029S");
                smtp.EnableSsl = true;
                Console.WriteLine("A/C Statement has been sent sucessfully");
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t\t\t\tFailed to send email");
            }
        }
    }
}
