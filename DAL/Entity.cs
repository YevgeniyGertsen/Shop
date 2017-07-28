using System.Data.Entity;


namespace DAL
{
    public class Entity : DbContext
    {
        public Entity() : base("DefaultConnection"){}

        public DbSet<User> Users { get; set; }
        public DbSet<VizitHistory> VisitHistory { get; set; }
        public DbSet<BlockHistory> BlockHistory { get; set; }

    }
}
