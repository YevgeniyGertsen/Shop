using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;


namespace DAL
{
    public class Entity : DbContext
    {
        public Entity() : base("DefaultConnection"){}
        public DbSet<User> Users { get; set; }
    }
}
