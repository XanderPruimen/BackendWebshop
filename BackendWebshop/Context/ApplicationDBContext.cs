using Microsoft.EntityFrameworkCore;
using BackendWebshop.DTO_s;

namespace BackendWebshop.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

            public DbSet<ItemDTO> Items { get; set; }
            public DbSet<UserDTO> users { get; set; }
            public DbSet<OrderDTO> orders { get; set; }

        
    }
}
