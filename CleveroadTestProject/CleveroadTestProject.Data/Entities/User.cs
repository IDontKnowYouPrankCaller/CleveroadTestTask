namespace CleveroadTestProject.Data.Entities
{
    #region namespaces
    using CleveroadTestProject.Infrastructure;
    #endregion

    public class User : EntityBase<int>
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
