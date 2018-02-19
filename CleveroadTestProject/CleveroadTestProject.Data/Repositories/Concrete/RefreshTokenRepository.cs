namespace CleveroadTestProject.Data.Repositories
{
    #region namespaces
    using CleveroadTestProject.Data.Entities;
    using CleveroadTestProject.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    #endregion

    public class RefreshTokenRepository : RepositoryBase<RefreshToken, int>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DbContext context) : base(context)
        {
        }

        public RefreshToken GetByToken(string token)
        {
            return this.dbSet.FirstOrDefault(t => t.Token == token);
        }

        public void Expire(RefreshToken token)
        {
            token.ExpirationDate = DateTime.UtcNow;
            this.Put(token);
        }

        public void ExpireUser(int userId)
        {
            var tokens = this.dbSet.Where(token => token.UserId == userId);

            foreach (var token in tokens)
            {
                token.ExpirationDate = DateTime.UtcNow;
            }

            this.dbContext.SaveChanges();
        }
    }
}
