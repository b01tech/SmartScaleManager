namespace Application.Services.Encrypter;

internal class Encrypter : IEncrypter
{
    public string Encrypt(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}
