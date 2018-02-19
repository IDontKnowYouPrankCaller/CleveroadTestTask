namespace CleveroadTestProject.Infrastructure
{
    public class AuthenticationSettings
    {
        public long AccessTokenLifespan { get; set; }
        public long RefreshTokenLifespan { get; set; }
        public string Secret { get; set; }
    }
}
