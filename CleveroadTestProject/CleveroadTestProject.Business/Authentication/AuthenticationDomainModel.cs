namespace CleveroadTestProject.Business
{
    #region namespaces
    using CleveroadTestProject.ViewModel.Authentication;
    using CleveroadTestProject.Data.Repositories;
    using CleveroadTestProject.Data.Entities;
    using CleveroadTestProject.Business.Authentication;
    using CleveroadTestProject.Infrastructure.Exceptions;
    using CleveroadTestProject.Infrastructure;
    using System;
    using Microsoft.Extensions.Options;
    #endregion

    public class AuthenticationDomainModel : IAuthenticationDomainModel
    {
        private AuthenticationSettings _settings;
        private IHashingStrategy _hashingStrategy;
        private ITokenGenerator _tokenGenerator;
        private IUserRepository _userRepository;
        private IRefreshTokenRepository _tokenRepository;

        public AuthenticationDomainModel(IOptions<AuthenticationSettings> settings,
                                         IHashingStrategy hashingStrategy,
                                         ITokenGenerator tokenGenerator,
                                         IUserRepository userRepository,
                                         IRefreshTokenRepository tokenRepository)
        {
            this._settings = settings.Value;
            this._hashingStrategy = hashingStrategy;
            this._tokenGenerator = tokenGenerator;
            this._userRepository = userRepository;
            this._tokenRepository = tokenRepository;
        }

        public TokenResponse Login(LoginRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                PasswordHash = this._hashingStrategy.GetHash(request.Password)
            };

            int? userId = this._userRepository.CheckAccess(user);
            bool isAccessAllowed = userId > 0;

            if (!isAccessAllowed)
            {
                throw new ApiException(ResponseCode.FailedLogin, "Invalid username or password");
            }

            return this.CreateToken(userId.Value);
        }

        public void Register(LoginRequest request)
        {
            if (this._userRepository.UsernameExists(request.Username))
            {
                throw new ApiException(ResponseCode.UsedUsername, "Requested username is already in use");
            }

            var user = new User
            {
                Username = request.Username,
                PasswordHash = this._hashingStrategy.GetHash(request.Password)
            };

            this._userRepository.Post(user);
        }

        public TokenResponse Refresh(string token)
        {
            var refreshToken = this._tokenRepository.GetByToken(token);

            bool isValidToken = refreshToken != null;
            if (!isValidToken)
            {
                throw new ApiException(ResponseCode.InvalidToken, "Invalid refresh token");
            }

            bool isActiveToken = refreshToken.ExpirationDate > DateTime.UtcNow;
            if (!isActiveToken)
            {
                throw new ApiException(ResponseCode.ExpiredToken, "Refresh token has expired");
            }

            this._tokenRepository.Expire(refreshToken);
            return this.CreateToken(refreshToken.UserId);
        }

        public void Logout(string token)
        {
            var refreshToken = this._tokenRepository.GetByToken(token);

            bool isValidToken = refreshToken != null;
            if (!isValidToken)
            {
                throw new ApiException(ResponseCode.InvalidToken, "Invalid refresh token");
            }

            this._tokenRepository.ExpireUser(refreshToken.UserId);
        }

        public TokenResponse CreateToken(int userId)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = this._tokenGenerator.GenerateNewToken(),
                ExpirationDate = DateTime.UtcNow.AddSeconds(this._settings.RefreshTokenLifespan)
            };

            this._tokenRepository.Post(refreshToken);

            var accessToken = this._tokenGenerator.GenerateSecurityToken(_settings, refreshToken.Token);

            return accessToken;
        }
    }
}
