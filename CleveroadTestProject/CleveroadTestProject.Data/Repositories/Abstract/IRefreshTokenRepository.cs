namespace CleveroadTestProject.Data.Repositories
{
    #region namespaces
    using CleveroadTestProject.Infrastructure;
    using CleveroadTestProject.Data.Entities;
    #endregion

    public interface IRefreshTokenRepository : IRepository<RefreshToken, int>
    {
        RefreshToken GetByToken(string token);
        void Expire(RefreshToken token);
        void ExpireUser(int userId);
    }
}
