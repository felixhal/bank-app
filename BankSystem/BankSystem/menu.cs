using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class menu
    {
        public int option = 0;

        //Displays main menu 
        public void mainMenu()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\t\tWELCOME TO SIMPLE BANKING SYSTEM");
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\t1. Create New Account");
            Console.WriteLine("\t\t\t\t2. Serach for Account");
            Console.WriteLine("\t\t\t\t3. Deposit");
            Console.WriteLine("\t\t\t\t4. Withdraw");
            Console.WriteLine("\t\t\t\t5. A/C Statement");
            Console.WriteLine("\t\t\t\t6. Delete Account");
            Console.WriteLine("\t\t\t\t7. Exit");
            Console.WriteLine("");
            Console.WriteLine("\t\t\t\t---------------------------------------------");
            Console.WriteLine("\t\t\t\tEnter your choice (1-7): ");
            Console.Write("\n\t\t\t\t---------------------------------------------\n\t\t\t\t");
            check();
        }

        //Asks users to enter a number 1-7
        void check()
        {
            try
            {
                int x1 = Console.CursorLeft;
                int y1 = Console.CursorTop;
                option = Convert.ToInt32(Console.ReadLine());
                Console.SetCursorPosition(x1, y1);
                directory();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t\t\t\tPLEASE ENTER VALID NUMBER (1-7)");
                Console.ReadKey();
                Console.Clear();
                mainMenu();
            }
        }

        void directory()
        {
            //If users inputs 1 than load Create Account
            if (option == 1)
            {
                Console.Clear();
                CreateAccount c1 = new CreateAccount();
                c1.inputFields();
                exit();
            }
            //If users inputs 2 than load Search Account
            else if (option == 2)
            {
                Console.Clear();
                SearchForAccount s1 = new SearchForAccount();
                s1.inputSearch();
                exit();
            //If users inputs 3 than load Deposit
            }
            else if (option == 3)
            {
                Console.Clear();
                Deposit d1 = new Deposit();
                d1.inputDeposit();
                exit();
                //If users inputs 4 than load Withdraw
            }
            else if (option == 4)
            {
                Console.Clear();
                Withdraw w1 = new Withdraw();
                w1.inputWithdraw();
                exit();
            }
            //If users inputs 5 than load Statement
            else if (option == 5)
            {
                Console.Clear();
                Statement st1 = new Statement();
                st1.inputSearch();
                exit();
            //If users inputs 6 than load Delete
            }
            else if (option == 6)
            {
                Console.Clear();
                Delete d1 = new Delete();
                d1.inputDelete();
                exit();
            //If users inputs 7 than close console
            }
            else if (option == 7)
            {
                Environment.Exit(0);
            }
            //If users input anything else than gives 
            else
            {
                Console.WriteLine("This is not correct");
                mainMenu();
            }
        }
        public void exit()
        {
            mainMenu();
        }
    }
}
