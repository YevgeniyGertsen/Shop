using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Users.Interfaces;
using Users.Repository;


namespace Shop
{
    class Program
    {
       private static IUserRepository ur = new UserRepository();

        static void Main(string[] args)
        {
            string logmes = "";
           // ur.DisableUser(1136, false, "", 1, out logmes);
            Console.Write("Введите логин: " );
            string login = Console.ReadLine();
            Console.WriteLine(  );
            Console.Write( "Введите пароль: " );
            string pass = Console.ReadLine();
           
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
