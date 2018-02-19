namespace CleveroadTestProject.Business
{
    #region namespaces
    using CleveroadTestProject.ViewModel.Authentication;
    #endregion

    public interface IAuthenticationDomainModel
    {
        TokenResponse Login(LoginRequest request);
        void Register(LoginRequest request);
        TokenResponse Refresh(string token);
        void Logout(string token);
        TokenResponse CreateToken(int userId);
    }
}
