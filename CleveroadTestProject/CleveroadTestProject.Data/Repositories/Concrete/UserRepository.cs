namespace CleveroadTestProject.Data.Repositories
{
    #region namespaces
    using CleveroadTestProject.Data.Entities;
    using CleveroadTestProject.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    #endregion

    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public int? CheckAccess(User user)
        {
            return this.dbSet
                       .Where(u => u.Username == user.Username && u.PasswordHash == user.PasswordHash)
                       .Select(u => u.Id)
                       .FirstOrDefault();
        }

        public bool UsernameExists(string username)
        {
            return this.dbSet.Any(x => x.Username == username);
        }
    }
}
