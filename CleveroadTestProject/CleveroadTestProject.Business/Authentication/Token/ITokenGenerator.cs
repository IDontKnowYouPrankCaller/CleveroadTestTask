namespace CleveroadTestProject.Business.Authentication
{
    #region namespaces
    using CleveroadTestProject.Infrastructure;
    using CleveroadTestProject.ViewModel.Authentication;
    #endregion

    public interface ITokenGenerator
    {
        string GenerateNewToken();
        TokenResponse GenerateSecurityToken(AuthenticationSettings settings, string refreshToken);
    }
}
