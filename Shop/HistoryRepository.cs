using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Shop
{
    class HistoryRepository
    {
        private Entity db = new Entity();
        public void AddHistory(History hist, out string errMessage)
        {
            errMessage = "Запись успешно добавлена";
            try
            {
                db.VisitHistory.Add(hist);
                db.SaveChanges();
                return;
            }
            catch (Exception e)
            {
                errMessage = "Ошибка добавления записи";
                return;
            }
        }
        public History GetUserHistory(int userId)
        {
            History history = new History();
            return history;
        }
    }
}
