using System.Collections.Generic;
using DAL;

namespace Users.Interfaces
{
    public interface IHistoryRepository : IEntityService
    {
        void AddHistory(VizitHistory hist, out string errMessage);
        List<VizitHistory> GetUserHistory(int userId);

        void AddBlockEvent(BlockHistory bEvent, out string errMessage);
        List<BlockHistory> GetBlockHistory(int userId);
    }
}
