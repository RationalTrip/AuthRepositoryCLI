namespace AuthRepository
{
    static class SqlQueries
    {
        public static string IsLoginAuthExistQuery => "SELECT COUNT(*) FROM [LoginAuth] WHERE [Login] = @login";
        public static string InsertLoginAuthQuery => "INSERT INTO [LoginAuth] ([Login], [Password], [Salt])  VALUES (@login, @password, @salt)";
        public static string SelectUserByLogin => "SELECT [AuthId], [Login], [Password], [Salt] FROM [LoginAuth] WHERE [Login] = @login";
    }
}
