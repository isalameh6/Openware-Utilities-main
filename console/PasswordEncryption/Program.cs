//TODO: accept options


using Microsoft.Extensions.Options;

using Openware.Security.Encryption;

var encryptionKey = RequestRequiredUserValue("Please write the encryption key:");


var selectedOption = RequesteOperationType();

if (selectedOption == "1")
{
    var password = RequestRequiredUserValue("Please write the text you wish to secure:");
    var encryptedText = processPasswordEncryption(encryptionKey, password);
    
    Console.WriteLine($"The encrypted value is:{encryptedText}");
}
else
{
    var encryptedPassword = RequestRequiredUserValue("Please write the text you wish to decrypt:");
    var decryptedText = processPasswordDecryption(encryptionKey, encryptedPassword);

    Console.WriteLine($"The decrypted value is:{decryptedText}");

}



Console.WriteLine("Press Any Key to close");

string processPasswordEncryption(string encryptionKey,string password)
{
    IDataEncryption dataEncryption = new TripleDESDataEncryption(Options.Create(new EncryptionOptions()
    {
        Key = encryptionKey
    }));

    return dataEncryption.Encrypt(password);
}

string processPasswordDecryption(string encryptionKey, string encryptedPassword)
{
    IDataEncryption dataEncryption = new TripleDESDataEncryption(Options.Create(new EncryptionOptions()
    {
        Key = encryptionKey
    }));

    return dataEncryption.Decrypt(encryptedPassword);
}

string RequestRequiredUserValue(string message)
{
    Console.Write(message);
    var value = Console.ReadLine();

    while (string.IsNullOrEmpty(value?.Trim()))
    {
        Console.Write(message);
        value = Console.ReadLine();
    }

    return value;
}

string RequesteOperationType()
{
    var message = "Please select [1] for encryption,[2] for decryption:";
    Console.Write(message);

    var value = Console.ReadLine();

    var validValues = new string[] { "1", "2" };

    while (string.IsNullOrEmpty(value?.Trim()) || !validValues.Contains(value) )
    {
        Console.Write(message);
        value = Console.ReadLine();
    }

    return value;
}

