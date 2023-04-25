using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

public static class HashPassword
{
    public static string CreateHash(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the password and salt using PBKDF2
        byte[] hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 32);

        // Combine the salt and hash into a single string
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);
        string hashedPassword = Convert.ToBase64String(hashBytes);

        return hashedPassword;
    }

    public static bool VerifyHash(string password, string hashedPassword)
    {
        // Extract the salt and hash from the stored password
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);
        byte[] hash = new byte[32];
        Array.Copy(hashBytes, 16, hash, 0, 32);

        // Compute a hash of the provided password using the stored salt
        byte[] computedHash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 32);

        // Compare the computed hash with the stored hash
        for (int i = 0; i < 32; i++)
        {
            if (hash[i] != computedHash[i])
            {
                return false;
            }
        }
        return true;
    }
}
