namespace CleveroadTestProject.Business.Authentication
{
    #region namespaces
    using System;
    using System.Security.Cryptography;
    using System.Text;
    #endregion

    public class SHA256HashingStrategy : IHashingStrategy
    {
        public string GetHash(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password), 0, Encoding.ASCII.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
