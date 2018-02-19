namespace CleveroadTestProject
{
    #region namespaces
    using Microsoft.Extensions.DependencyInjection;
    using CleveroadTestProject.Data;
    using CleveroadTestProject.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    #endregion

    public partial class Startup
    {
        public void ConfigureData(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<DbContext, ApplicationDbContext>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}
