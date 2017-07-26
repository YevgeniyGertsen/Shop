using System.Data.Entity;

namespace DAL
{
    public class Entity : DbContext
    {
        public Entity() : base("DefaultConnection"){}

        public DbSet<User> Users { get; set; }
    }
}
