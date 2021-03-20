using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace BankSystem
{
    class login
    {
            public String password = "";
            public String username = "";
            ArrayList users = new ArrayList();

            //Asks users for details
            public void details()
            {
                //Banking System login user interface
                Console.WriteLine("\t\t\t\t---------------------------------------------");
                Console.WriteLine("\t\t\t\t\tWELCOME TO SIMPLE BANKING SYSTEM");
                Console.WriteLine("\t\t\t\t---------------------------------------------");
                Console.Write("\t\t\t\tUsername: ");
                //Captures Cursor position for Username
                int CursorPostitionX1 = Console.CursorLeft;
                int CursorPositionY1 = Console.CursorTop;

                Console.Write("\n\t\t\t\tPassword: ");
                //Captures Cursor position for Password
                int CursorPostitionX2 = Console.CursorLeft;
                int CursorPositionY2 = Console.CursorTop;

                //Set cursor position for username
                Console.SetCursorPosition(CursorPostitionX1, CursorPositionY1);
                username = Console.ReadLine();
                //Set cursosr position for password
                Console.SetCursorPosition(CursorPostitionX2, CursorPositionY2);
                //prompts for password
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    // Does not accept backspace and enter key as an input
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        //Concatenates into password
                        password += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        //Backspace deletes a char from password
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
                listOfDetails();
            }

            //reads login.txt file and inputs into Arraylist
            public void listOfDetails()
            {
                //Opens login.txt file
                String userDetails;
                StreamReader file = new StreamReader("login.txt");
                //Reads it line by line until a line outputs null
                while ((userDetails = file.ReadLine()) != null)
                {
                    users.AddRange(userDetails.Split('|'));
                }
                check();
            }

            //checks if username and pasword exist in the the users arraylist
            public void check()
            {
                //check if username and passworrd is in users and matches password and user through index
                if (users.Contains(username) && users.IndexOf(password) == users.IndexOf(username) + 1)
                {
                    //Confirmation message of successful attempt
                    Console.WriteLine("\n\t\t\t\tLogin successful");
                    Console.ReadKey();
                }
                else
                {
                    //Error message of unsucessful attempt
                    Console.WriteLine("\n\t\t\t\tlogin fail, please try again ");
                    password = "";
                    Console.ReadKey();
                    Console.Clear();
                    details();
                }
            }
        }
}
