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
    class CreateAccount
    {
        menu m1 = new menu();
        String firstName, lastName, address, email, phone;
        int phoneNum, balance = 0;

        int CursorPositionX1;
        int CursorPositionX2;
        int CursorPositionX3;
        int CursorPositionX4;
        int CursorPositionX5;
        int CursorPositionX6;

        int CursorPositionY1;
        int CursorPositionY2;
        int CursorPositionY3;
        int CursorPositionY4;
        int CursorPositionY5;
        int CursorPositionY6;
        //Asks users to enter fields required to create account
        public void inputFields()
        {
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\t\t\tCREATE ACCOUNT");
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\tPLEASE ENTER DETAILS");
            Console.Write("\n\t\t\t\tFirst Name: ");
            CursorPositionX1 = Console.CursorLeft;
            CursorPositionY1 = Console.CursorTop;
            Console.Write("\n\t\t\t\tLast Name : ");
            CursorPositionX2 = Console.CursorLeft;
            CursorPositionY2 = Console.CursorTop;
            Console.Write("\n\t\t\t\tAddress   : ");
            CursorPositionX3 = Console.CursorLeft;
            CursorPositionY3 = Console.CursorTop;
            Console.Write("\n\t\t\t\temail     : ");
            CursorPositionX4 = Console.CursorLeft;
            CursorPositionY4 = Console.CursorTop;
            Console.Write("\n\t\t\t\tphone     : ");
            CursorPositionX5 = Console.CursorLeft;
            CursorPositionY5 = Console.CursorTop;
            Console.Write("\n\t\t\t\t---------------------------------------------");
            CursorPositionX6 = Console.CursorLeft;
            CursorPositionY6 = Console.CursorTop;

            firstName = promptUser(CursorPositionX1, CursorPositionY1);
            lastName = promptUser(CursorPositionX2, CursorPositionY2);
            address = promptUser(CursorPositionX3, CursorPositionY3);
            email = promptUser(CursorPositionX4, CursorPositionY4);
            phone = promptUser(CursorPositionX5, CursorPositionY5);

            checkEmail();
        }

        //prompts users for input based on the recorder cursor position
        public String promptUser(int x, int y)
        {
            String input;
            Console.SetCursorPosition(x, y);
            input = Console.ReadLine();
            return input;
        }

        //Checks Email field
        public void checkEmail()
        {
            //Check if email contains @ and .com or .edu.au
            if (email.Contains("@") && (email.Contains(".com") || email.Contains(".edu.au")))
            {
            }
            else
            {
                Console.WriteLine("\n\t\t\t\tPlease enter a valid email address");
                reset();
            }
            //Check if email is gmail, hotmail or uts email address
            if (email.Contains("gmail") || email.Contains("aol") || email.Contains("uts") || email.Contains("hotmail"))
            {
            }
            else
            {
                Console.WriteLine("\n\t\t\t\tPlease use only gmail, hotmail, aol or uts mail");
                reset();
            }
            checkPhone();
        }

        //checks phone field
        public void checkPhone()
        {
            //Check if phone number is less than 10 characters long
            if (!(phone.Length < 10))
            {
                Console.WriteLine("\n\t\t\t\tPlease enter valid phone number (under 10 charracter long)");
                reset();
            }
            //Check if any phone number is inputed
            else if (phone.Length == 0)
            {
                Console.WriteLine("\n\t\t\t\tPlase enter a phone number");
                reset();
            }
            else
            {
                try
                {
                    //Convert phoneNum to int
                    phoneNum = Convert.ToInt32(phone);
                    keepDetails();
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t\t\t\tPlease enter valid phone number (under 10 charracter long)");
                    reset();
                }
            }
        }

        //Keep account details in .txt file 
        public void keepDetails()
        {
            //Assigns account 6 bit account number 
            int AccountNum = genAccountNum(100000, 999999);
            //Stores details in Accounts arrays
            String[] Accounts = { "firstName:" + firstName, "lastName:" + lastName, "address:" +
                    address, "phone:" + phone, "email:" + email, "Balance: " + balance};

            //Creates file containing element of array Accounts with AccountNum as name
            System.IO.File.WriteAllLines(AccountNum + ".txt", Accounts);
            //Confirmation message
            Console.WriteLine("\n\t\t\t\tYour Account number is: " + AccountNum);
            Console.WriteLine("\n\t\t\t\tAccount Details have been saved and will be sent via email");
            String details = printDetails(AccountNum);
            emailStatement(details, AccountNum.ToString());
            Console.ReadKey();
            m1.mainMenu();
        }

        public String printDetails(int AccountNum)
        {
            string[] Accounts = File.ReadAllLines(AccountNum + ".txt");
            String details = "";
            //foreach loops iterates through each string in the Accounts array
            for (int loopVar = 0; loopVar < 6; loopVar++)
            {
                //prints out each string in Account array
                details += Accounts[loopVar] + "\n";
            }
            return details;
        }

        //function returns a Unique Account number
        public int genAccountNum(int min, int max)
        {
            Random AccountNum = new Random();
            return AccountNum.Next(min, max);
        }


        //reset console interface
        public void reset()
        {
            Console.WriteLine("\n\t\t\t\tPress any key to continue");
            Console.ReadKey();
            Console.Clear();
            inputFields();
        }

        public void emailStatement(String content, String AccountNum)
        {
            try
            {
                MailAddress to = new MailAddress(email);

                MailAddress from = new MailAddress("dummyfor.appdev.dotnet@gmail.com");

                MailMessage mail = new MailMessage(from, to);

                mail.Subject = "Your Account Details " + AccountNum;

                mail.Body = content;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;

                smtp.Credentials = new NetworkCredential(
                    "dummyfor.appdev.dotnet@gmail.com", "ut$!333029S");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Console.WriteLine("\n\t\t\t\temail sucessfully sent, Press any key to continue");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t\t\t\tFailed to send email");
            }
        }
    }
}
