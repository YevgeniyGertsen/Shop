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
        static void Main(string[] args)
        {

          
            UserRepository ur = new UserRepository();
            
           User t =  ur.GetUserInfo();
          
                t.Login = "ddd";

            ur.ResetPassword("", User.TypeReset.login);
            string errMess;
            ur.EditUser(t,out errMess);
            Console.WriteLine(errMess);
        }
    }
}
