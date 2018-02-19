namespace CleveroadTestProject.ViewModel.Authentication
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }
}
