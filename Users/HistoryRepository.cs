using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace ServiceUsers
{
    class HistoryRepository
    {
        private Entity db = new Entity();
        public void AddHistory(VizitHistory hist, out string errMessage)
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
        public List<VizitHistory> GetUserHistory(int userId)
        {
            List<VizitHistory> history = db.VisitHistory.Where(w => w.UserID == userId).ToList();
            return history;
        }
        public void AddBlockEvent(BlockHistory bEvent, out string errMessage)
        {
            errMessage = "Событие успешно добавлено";
            try
            {
                if (bEvent.BlockUnblockDate == null)
                    bEvent.BlockUnblockDate = DateTime.Now;
                db.BlockHistory.Add(bEvent);
                db.SaveChanges();
                return;
            }
            catch (Exception e)
            {
                errMessage = "Ошибка добавления записи";
                return;
            }
        }
        public List<BlockHistory> GetBlockHistory(int userId)
        {
            List<BlockHistory> blockHistory = db.BlockHistory.Where(w => w.BlockUserID == userId).ToList();
            return blockHistory;
        }
    }
}
