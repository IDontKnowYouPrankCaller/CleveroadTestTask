namespace CleveroadTestProject.Business.Authentication
{
    public interface IHashingStrategy
    {
        string GetHash(string password);
    }
}
