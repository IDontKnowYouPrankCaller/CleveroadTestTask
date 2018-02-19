namespace CleveroadTestProject.Data.Repositories
{
    #region namespaces
    using CleveroadTestProject.Infrastructure;
    using CleveroadTestProject.Data.Entities;
    #endregion

    public interface IUserRepository : IRepository<User, int>
    {
        int? CheckAccess(User user);
        bool UsernameExists(string username);
    }
}
