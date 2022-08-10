namespace AuthRepository
{
    public interface IPasswordHasher
    {
        string HashPassword(string salt, string password);
    }
}