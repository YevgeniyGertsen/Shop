using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Users")]
    public class User
    {
        public string iin;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegDate { get; set; }
        public string User1CId { get; set; }
        public string IIN
        {
            get { return iin; }
            set
            {
                if (!string.IsNullOrEmpty(iin))
                {
                    if (iin.Length > 12)
                        iin = value.Substring(0, 11);
                    else
                        iin = value;
                }
                else
                    iin = "880111300392";
            }

        }
        public string City1CGuid { get; set; }
        public int CityId { get; set; }
        public string Contr1CId { get; set; }
        public string ContrCode { get; set; }
        public Nullable<System.Guid> Contract1CId { get; set; }
    }
}
