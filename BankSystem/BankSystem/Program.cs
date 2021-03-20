using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            login l1 = new login();
            l1.details();
            menu m1 = new menu();
            m1.mainMenu();
            Console.ReadKey();
        }
    }
}
