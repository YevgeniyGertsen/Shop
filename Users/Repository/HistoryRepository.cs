using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Users.Interfaces;

namespace Users.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private Entity db = new Entity();
        
        public void AddHistory(User user, out string errMessage)
        {
            VizitHistory hist = new VizitHistory()
            {
                ClientAgent = "Chrome",
                ClientDevice = "Win",
                IP = "",
                UserID = user.Id,
                VizitDate = DateTime.Now
            };

            errMessage = "Запись успешно добавлена";
            try
            {
                db.VisitHistory.Add(hist);
            }
            catch (Exception e)
            {
                errMessage = "Ошибка добавления записи";
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
                if (bEvent.CreateDate == null)
                    bEvent.CreateDate = DateTime.Now;
                db.BlockHistory.Add(bEvent);
            }
            catch (Exception e)
            {
                errMessage = "Ошибка добавления записи";
            }
        }
        public List<BlockHistory> GetBlockHistory(int userId)
        {
            List<BlockHistory> blockHistory = db.BlockHistory.Where(w => w.BlockUserID == userId).ToList();
            return blockHistory;

        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
