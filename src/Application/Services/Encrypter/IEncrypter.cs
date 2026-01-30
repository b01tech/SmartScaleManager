namespace Application.Services.Encrypter;

public interface IEncrypter
{
    string Encrypt(string password);
    bool Verify(string password, string hashedPassword);
}
