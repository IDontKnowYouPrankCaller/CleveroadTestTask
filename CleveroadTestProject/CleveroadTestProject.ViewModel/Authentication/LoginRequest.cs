namespace CleveroadTestProject.ViewModel.Authentication
{
    #region namespaces
    using System.ComponentModel.DataAnnotations;
    #endregion

    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
