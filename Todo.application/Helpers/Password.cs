using BC = BCrypt.Net.BCrypt;

namespace Todo.application.Helpers.Hashing;

public static class Pwd
{
    private const int WORKFACTOR = 13;
    public static string Hash(string pwd)
    {
        return BC.EnhancedHashPassword(pwd, WORKFACTOR);
    }

    public static bool Verify(string pwd, string hash)
    {
        return BC.EnhancedVerify(pwd, hash);
    }
}
