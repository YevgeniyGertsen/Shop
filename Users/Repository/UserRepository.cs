using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using DAL;
using Users.Interfaces;

namespace Users.Repository
{
    public class UserRepository: IUserRepository
    {
        IHistoryRepository hr;
        private readonly Entity db;
        private string outMess = "";

        public delegate void dMessage(string mes);
        private dMessage del= new dMessage(ShowMessage);

        public delegate void dHistory(VizitHistory hist, out string str);

        private dHistory dHis;

        public void dHistRegister(dHistory dhis)
        {
            dHis = dhis;
        }
        public void dHistRegister2(dHistory dhis)
        {
            dHis -= dhis;
        }

        public UserRepository()
        {
            hr=new HistoryRepository();
            db = new Entity();
          

        }

        public User GetUserInfo(HttpRequestBase request = null)
        {
            User u = new User();
            if (request == null)
            {
                u = db.Users.FirstOrDefault(w => w.Id == 1131);
                if (u == null)
                {
                    u = new User();
                }
            }
            else if (request.IsAuthenticated)
            {
                u.RegDate = DateTime.Now;
                u.Email = "serg@mail.ru";
                var t = u.IIN;

                u.Name = "Sergey";
            }

            return u;
        }

        public User GetUserInfo(int id)
        {
            del.Invoke("Получили информацию о пользователе по его ID");
            return db.Users.FirstOrDefault(w => w.Id == id);
        }

        public User GetUserInfo(string email)
        {
            del.Invoke("Получили информацию о пользователе по его email");
            return db.Users.FirstOrDefault(w => w.Email == email);
        }

        public void RegisterDelegate(dMessage dMes)
        {
            del = dMes;
        }

        public bool EditUser(User user)
        {
           // del = ShowMessageEditUser;

            bool result = true;
            string errMessage = "Данные изменены.";

            User findUser = db.Users.Find(user.Id);
            //findUser.Login = user.Login;
            findUser.Name = user.Name;
            findUser.Email = user.Email;
            //findUser.Password = user.Password;
            findUser.Phone = user.Phone;
            findUser.IIN = user.IIN;

            try
            {
                db.Entry(findUser).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                errMessage = e.Message;
                
                result= false;
            }
            finally
            {
                del.Invoke(errMessage);
                //dSm(ShowMessageEditUser);
            }

            return result;
        }

        private static void ShowMessage(string mes)
        {
            Console.WriteLine(mes);
        }
        private static void ShowMessageEditUser(string mes)
        {
            Console.WriteLine("Результат изменения пользователя: "+ mes);
        }

        private static void dSm(dMessage d)
        {
            d.Invoke("Работа завершена");
        }

        public User AddUser(User user, out string errMessage)
        {
            errMessage = "Успешное добавление";
            try
            {
                user.IsBlocked = false;
                user.IsDeleted = false;
                user.RegDate = DateTime.Now;
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

        public bool DeleteUser(int id, out string erMessage)
        {
            erMessage = "Удаление прошло успешно";
            try
            {
                User tempUser = db.Users.Find(id);
                if (tempUser == null)
                {
                    erMessage = "Запрашиваемый польззователь не найден";
                    return false;
                }

                tempUser.IsDeleted = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                erMessage = $"Не удаётся удалить пользователя: {e.Message}";
                return false;
            }

        }

        public bool DisableUser(int id, bool isBlocked, string blockReason, int blockInitiator, out string erMessage)//добавить причину блокировки и кто заблокировал
        {
            erMessage =( isBlocked ? "Отключение" : "Включение") + " пользователя прошло успешно";
            try
            {
                User tempUser = db.Users.Find(id);
                if (tempUser == null)
                {
                    erMessage = "Запрашиваемый пользователь не найден";
                    return false;
                }

                tempUser.IsBlocked = isBlocked;
                //tempUser.DateOfBlock = isBlocked ? DateTime.Now : null; //так выходит ошибка неявного преобразования
                if (isBlocked)
                {
                    tempUser.DateOfBlock = DateTime.Now;

                }
                else if (!isBlocked)
                {
                    tempUser.DateOfBlock = null;
                    tempUser.TryesCount = 0;
                }

                BlockHistory bh = new BlockHistory();
                bh.CreateDate= DateTime.Now;
                bh.Initiator = blockInitiator;
                bh.Reason = blockReason;
                bh.BlockUserID = id;

                hr.AddBlockEvent(bh, out erMessage);
                hr.Save();


                //добавить поле дата блокировки при блокирлвке, а при разблокировке - очистить поле
                //2 записать историю в таблицу Исторя блокировок
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                erMessage = e.Message;
                return false;
            }

        }

        public bool ResetPassword(string val, User.TypeReset typeReset, out string PassResetDoneConfirmation)
        {
            PassResetDoneConfirmation = "";
            try
            {
                switch (typeReset)
                {
                    case User.TypeReset.login:
                        {
                            var tUser = db.Users.Any(w => w.Login == val);
                            if (tUser)
                            {
                                User TempFindUser = db.Users.FirstOrDefault(w => w.Login == val);

                                string newPassword = RandomString(8);

                                TempFindUser.Password = GenerateMD5(newPassword);
                                db.SaveChanges();

                                string cutEmail = TempFindUser.Email.Substring(0, 3) + "***" + TempFindUser.Email.Substring(TempFindUser.Email.IndexOf('@'), TempFindUser.Email.Length);

                                SendMail("", new List<string>() { TempFindUser.Email }, "Смена пароля", string.Format("Ваш новый пароль {0}", newPassword));

                                PassResetDoneConfirmation = "Ваш новый пароль отправлен Вам на почту " + cutEmail;
                                return true;
                            }
                            else
                            {
                                PassResetDoneConfirmation = "Пользователь с таким логином не найден.";
                                return false;

                            }
                        }
                    case User.TypeReset.phone:
                        {
                            var tUser = db.Users.Any(w => w.Phone == val);
                            if (tUser)
                            {
                                User TempFindUser = db.Users.FirstOrDefault(w => w.Phone == val);

                                string newPassword = RandomString(8);

                                TempFindUser.Password = GenerateMD5(newPassword);
                                db.SaveChanges();

                                string cutEmail = TempFindUser.Email.Substring(0, 3) + "***" + TempFindUser.Email.Substring(TempFindUser.Email.IndexOf('@'), TempFindUser.Email.Length);

                                SendMail("", new List<string>() { TempFindUser.Email }, "Смена пароля", string.Format("Ваш новый пароль {0}", newPassword));

                                PassResetDoneConfirmation = "Ваш новый пароль отправлен Вам на почту " + cutEmail;
                                return true;
                            }
                            else
                            {
                                PassResetDoneConfirmation = "Пользователь с таким номером телефона не найден.";
                                return false;

                            }
                        }

                }
                return false;
            }
            catch (Exception e)
            {
                PassResetDoneConfirmation = "При смене пароля произошла ошибка. Попробуйте позже.";
                return false;
            }
        }

        public List<User> ReportUsers(User.UserReport ur, User.InactivePeriod period)
        {
            List<User> LU = new List<User>();

            switch (ur)
            {
                case User.UserReport.blockedUsers:
                    LU = db.Users.Where(w => w.IsBlocked).ToList();
                    break;
                case User.UserReport.unblockedUsers:
                    LU = db.Users.Where(w => !w.IsBlocked).ToList();
                    break;
                case User.UserReport.allUsers:
                    LU = db.Users.ToList();
                    break;
                case User.UserReport.inactiveUsers:
                    {
                        switch (period)
                        {
                            case User.InactivePeriod.SixMonth:
                                LU = db.Users.Where(w => w.LastLogonDate <= DateTime.Now.AddMonths(-6).Date).ToList();
                                break;
                            case User.InactivePeriod.Year:
                                LU = db.Users.Where(w => w.LastLogonDate <= DateTime.Now.AddYears(-1).Date).ToList();
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }

            return LU;
        }

        private static Random random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateMD5(string newPassword)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(newPassword);

            byte[] hash = md5.ComputeHash(inputBytes);

            //// step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();
        }

        private static void SendMail(string from, List<string> toList, string subject, string messageText)
        {
            if (@from == "")
            {
                @from = "support_PhaetonDC@phaeton.kz";
            }

            var msg = new MailMessage { From = new MailAddress(@from, "Интернет магазин Phaeton") };
            var to = toList.FirstOrDefault();
            msg.To.Add(new MailAddress(to));

            msg.Subject = subject;

            foreach (var item in toList.Where(w => w != to))
                msg.CC.Add(new MailAddress(item.Trim()));

            msg.Body = messageText;
            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;


            var smtpClient = new SmtpClient("webmail.phaeton.kz", 25) { EnableSsl = false };
            try
            {
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                var cc = ex.Message;
            }

        }

      public User UserLogon(string login, string password, out string logonMessage)
        {
            logonMessage = "";
            //8134B4FDE7DD5479C484FCE138AEA1E18FC09229
            string hash = GenerateMD5(password);
            User user = new User();
            User userByLogin = db.Users.FirstOrDefault(w => w.Login == login);
            if (userByLogin!=null)
            {

                if (userByLogin.IsBlocked)
                {
                    logonMessage = "Пользователь заблокирован";
                    return null;
                }

                if (userByLogin.TryesCount>=3)
                {
                    logonMessage = "Пользователь заблокирован";
                    string errMess = "";
                    DisableUser(userByLogin.Id, true,"Превышен лимит неверных попыток ввода пароля.", 1121, out errMess);
                    return null;
                }


                user = db.Users.FirstOrDefault(w => w.Login == login && w.Password == hash);
                if (user == null)
                {

                    logonMessage = "Неверный логин или пароль.";
                    db.Users.FirstOrDefault(w => w.Login == login).TryesCount += 1;
                  
                }
                else
                {
                    user.LastLogonDate = DateTime.Now;
                    user.TryesCount = 0;
                  

                    var msg = "";
                    if (dHis != null)
                        dHis.Invoke(user,out msg);

                }
                db.SaveChanges();
            }
            else
            {
                logonMessage = "Неверный логин или пароль.";
            }
           
            return user;
        }

        public void Save(){}
    }
}
