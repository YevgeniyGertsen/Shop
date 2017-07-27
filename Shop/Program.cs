using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ServiceUsers;


namespace Shop
{
    class Program
    {
       private static UserRepository ur = new UserRepository();

        static void Main(string[] args)
        {
            Console.Write("Введите логин: " );
            string login = Console.ReadLine();
            Console.WriteLine(  );
            Console.Write( "Введите пароль: " );
            string pass = Console.ReadLine();
            string logmes = "";
            User currUser = ur.UserLogon(login, pass, out logmes);

            if (currUser==null)
            {
                Console.WriteLine(logmes);
            }
            else
            {
                Console.WriteLine("Превед {0}", currUser.Name);
            }
            
        }
    }
}
