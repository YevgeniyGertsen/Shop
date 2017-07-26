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
        int id=0;
        string login;
        string password;
        string phone;
        string email;
        DateTime regDate;
        string user1CId;
        string iin;
        string city1CGuid;
        int cityId;
        string contr1CId;
        string contrCode;
        Guid? contract1CId;
        bool isBlocked;
        bool isDeleted;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegDate
        {
            get { return RegDate; }
            set
            {
                if (value==null)
                {
                    RegDate = DateTime.Now;
                }
            }
        }
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
        public int CityId {
            get
            { return cityId; }
         set
            {
                if (value==0 || value<0)
                {
                    cityId = 1;
                }
                else
                {
                    cityId = value;
                }
            }
        }
        public string Contr1CId { get; set; }
        public string ContrCode { get; set; }
        public Guid? Contract1CId { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }

        public void Setid(User u)
        {
            id = u.id;
        }
        public int Getid(User u)
        {
            return id;
        }
        //string login;
        //string password;
        //string phone;
        //string email;
        //DateTime regDate;
        //string user1CId;
        //string iin;
        //string city1CGuid;
        //int cityId;
        //string contr1CId;
        //string contrCode;
        //Guid? contract1CId;
        //bool isBlocked;
        //bool isDeleted;





    }
}
