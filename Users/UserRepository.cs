using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using DAL;


namespace ServiceUsers
{
    public class UserRepository
    {

        private string name = "";

        private Entity db = new Entity();

        public User GetUserInfo(HttpRequestBase request=null)
        {
            User u = new User();
            if (request == null)
            {
                u = db.Users.FirstOrDefault(w => w.Id == 1131);
                if (u==null)
                {
                    u = new User();
                }
            }
            else if (request.IsAuthenticated)
            {
                u.Email = "serg@mail.ru";
                u.Name = "Sergey";
            }
          
            return u;
        }

        public User GetUserInfo(int id)
        {
            return db.Users.FirstOrDefault(w => w.Id == id);
        }

        public User GetUserInfo(string email)
        {
            return db.Users.FirstOrDefault(w => w.Email == email);
        }

        public bool EditUser(User user, out string errMessage)
        {
            errMessage = "Данные изменены.";
          
            try
            {
                User u = db.Users.Find(user.Id);
                u = user;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                errMessage = e.Message;
                return false;
            }
            
            
        }

        public User addUser(User user, out string errMessage)
        {
            errMessage = "Успешное добавление";
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                errMessage = "Ошибка добавления пользователя";
                return null;
            }
           
        }
    }}
