namespace CleveroadTestProject.Data.Entities
{
    #region namespaces
    using CleveroadTestProject.Infrastructure;
    using System;
    #endregion

    public class RefreshToken : EntityBase<int>
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
    }
}
