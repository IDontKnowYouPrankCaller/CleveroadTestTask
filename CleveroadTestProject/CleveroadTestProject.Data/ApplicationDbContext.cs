namespace CleveroadTestProject.Data
{
    #region namespaces
    using CleveroadTestProject.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    #endregion

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
