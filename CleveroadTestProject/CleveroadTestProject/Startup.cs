namespace CleveroadTestProject
{
    #region namespaces
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;
    #endregion

    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAuthentication(services);

            ConfigureData(services);

            ConfigureDomainModels(services);

            services.AddMvc();

            services.AddOptions();

            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", new Info { Title = "Cleveroad Test Project", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(config => {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Cleveroad Test Project V1");
            });

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
