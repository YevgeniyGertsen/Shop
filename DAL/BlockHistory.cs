using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BlockHistory
    {
        DateTime blockUnblockDate;

        public int BlockUserID { get; set; }
        public string BlockUnblockReason { get; set; }
        public int BlockUnblockInitiator { get; set; }
        public DateTime BlockUnblockDate
        {
            get { return blockUnblockDate; }
            set
            {
                if (value == null || value == DateTime.MinValue)
                    blockUnblockDate = DateTime.Now;
                else
                    blockUnblockDate = value;
            }
        }
    }
}
