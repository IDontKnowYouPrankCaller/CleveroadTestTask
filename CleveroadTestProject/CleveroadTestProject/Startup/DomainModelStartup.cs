namespace CleveroadTestProject
{
    #region namespaces
    using Microsoft.Extensions.DependencyInjection;
    using CleveroadTestProject.Business;
    using CleveroadTestProject.Business.Authentication;
    #endregion

    public partial class Startup
    {
        public void ConfigureDomainModels(IServiceCollection services)
        {
            // Authentication
            services.AddTransient<IAuthenticationDomainModel, AuthenticationDomainModel>();
            services.AddTransient<IHashingStrategy, SHA256HashingStrategy>();
            services.AddTransient<ITokenGenerator, JwtTokenGenerator>();
        }
    }
}
