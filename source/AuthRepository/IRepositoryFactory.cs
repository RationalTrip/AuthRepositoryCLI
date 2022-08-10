namespace AuthRepository
{
    public interface IRepositoryFactory
    {
        IPasswordHasher GetPasswordHasher();
        ISaltGenerator GetSaltGenerator();
        ILoginAuthRepository GetLoginAuthRepository(string connectionString);
    }
}
