namespace CleveroadTestProject.Infrastructure
{
    public enum ResponseCode
    {
        Success = 200,
        FailedLogin = 401,
        UsedUsername = 402,
        InvalidToken = 403,
        ExpiredToken = 404,
        ValidationError = 405,
        UnhandledError = 500
    }
}
