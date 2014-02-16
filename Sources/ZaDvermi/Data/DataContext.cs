using System.Data.Entity;
using ZaDvermi.Models;

namespace ZaDvermi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<MediaItem> MediaItems { get; set; } 

        public DataContext() : base("ZaDvermiDatabase")
        {
            
        }
    }
}