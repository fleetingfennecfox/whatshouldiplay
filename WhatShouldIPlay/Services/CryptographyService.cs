using System;
using System.Security.Cryptography;

namespace WhatShouldIPlay.Services
{
    public class CryptographyService
    {
        public string GenerateRandomString()
        {
            byte[] bytes = new byte[(int)Math.Floor(15 * .75)];

            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return Convert.ToBase64String(bytes);
        }

        public string Hash(string original, string salt)
        {
            const int hashByteSize = 20;

            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] bytes;
            using (Rfc2898DeriveBytes pbkdf2= new Rfc2898DeriveBytes(original, saltBytes))
            {
                pbkdf2.IterationCount = 1;
                bytes = pbkdf2.GetBytes(hashByteSize);
            }

            return Convert.ToBase64String(bytes);
        }
    }
}