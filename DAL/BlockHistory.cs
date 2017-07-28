using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("BlockHistory")]
    public class BlockHistory
    {
        DateTime blockUnblockDate;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlockHistID { get; set; }
        public int BlockUserID { get; set; }
        public string Reason { get; set; }
        public int Initiator { get; set; }
        public DateTime CreateDate
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
