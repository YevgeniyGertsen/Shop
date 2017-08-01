using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using  DAL;
using Users.Repository;

namespace Users.Interfaces
{
    public interface IUserRepository : IEntityService
    {
        User GetUserInfo(HttpRequestBase request = null);
        User GetUserInfo(int id);
        User GetUserInfo(string email);
        bool EditUser(User user);
        User AddUser(User user, out string errMessage);
        bool DeleteUser(int id, out string erMessage);
        bool DisableUser(int id, bool isBlocked, string blockReason, int blockInitiator, out string erMessage);
        bool ResetPassword(string val, User.TypeReset typeReset, out string PassResetDoneConfirmation);
        List<User> ReportUsers(User.UserReport ur, User.InactivePeriod period);
        User UserLogon(string login, string password, out string logonMessage);
        void dHistRegister(UserRepository.dHistory dhis);

    }
}
