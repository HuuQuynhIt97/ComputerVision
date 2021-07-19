using CPVS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CPVS_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<RawDatas> RawDatas { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<TimeLine> TimeLines { get; set; }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingUser> BuildingUser { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasKey(x => x.ID);
        }

    }
}