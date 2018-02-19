namespace CleveroadTestProject.Web.Controllers
{
    #region namespaces
    using Microsoft.AspNetCore.Mvc;
    using CleveroadTestProject.ViewModel;
    using CleveroadTestProject.ViewModel.Authentication;
    using CleveroadTestProject.Infrastructure;
    using Microsoft.Extensions.Options;
    using CleveroadTestProject.Business;
    using Microsoft.AspNetCore.Authorization;
    #endregion

    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private AuthenticationSettings _authSettings { get; set; }
        private IAuthenticationDomainModel _domainModel { get; set; }

        public AuthController(IOptions<AuthenticationSettings> authSettings,
                              IAuthenticationDomainModel domainModel)
        {
            this._authSettings = authSettings.Value;
            this._domainModel = domainModel;
        }

        [HttpPost(nameof(Login))]
        public ResponseBase Login([FromBody]LoginRequest request)
        {
            base.ValidateModel();
            var response = this._domainModel.Login(request);

            return base.Success("Login successfuly completed", response);
        }

        [HttpPost(nameof(Register))]
        public ResponseBase Register([FromBody]LoginRequest request)
        {
            base.ValidateModel();
            this._domainModel.Register(request);

            return base.Success("Registration successfuly completed");
        }

        [HttpPost(nameof(RefreshToken))]
        public ResponseBase RefreshToken([FromBody]string token)
        {
            var response = this._domainModel.Refresh(token);

            return base.Success("Token successfuly refreshed", response);
        }

        [HttpDelete(nameof(Logout))]
        public ResponseBase Logout(string token)
        {
            this._domainModel.Logout(token);

            return base.Success("Logout successfully completed");
        }
    }
}