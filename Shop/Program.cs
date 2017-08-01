using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using NLog;
using Users.Interfaces;
using Users.Repository;


namespace Shop
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static IUserRepository ur = new UserRepository();
        private static HistoryRepository hr = new HistoryRepository();
        static void Main(string[] args)
        {
            string logmes = "";
            // ur.DisableUser(1136, false, "", 1, out logmes);
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Введите пароль: ");
            string pass = Console.ReadLine();

            ur.dHistRegister(MyMethod);
            User currUser = ur.UserLogon(login, pass, out logmes);

            // if (currUser==null)
            // {
            //     Console.WriteLine(logmes);
            // }
            // else
            // {
            //     Console.WriteLine("Превед {0}", currUser.Name);
            // }

            //UserRepository ur = new UserRepository();
            //User u = ur.GetUserInfo(1136);
            //u.IIN = "123456789012";

            //ur.RegisterDelegate(WriteLog);

            //bool t = ur.EditUser(u);

            Console.ReadLine();

        }

        public static void MyMethod(VizitHistory hist, out string errMessage)
        {
            errMessage = "Запись успешно добавлена";
           Console.WriteLine("TEST");
        }
        public static void  WriteLog(string logMes)
        {
            logger.Error(logMes);
        }
    }
}
