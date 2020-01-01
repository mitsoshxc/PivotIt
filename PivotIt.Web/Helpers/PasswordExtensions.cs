using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace PivotIt.Web.Helpers
{
    public static class PasswordExtensions
    {
        public static byte[] GenerateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public static string GenerateHash(string password, byte[] salt)
        {
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                    password: password,
                                    salt: salt,
                                    prf: KeyDerivationPrf.HMACSHA1,
                                    iterationCount: 10000,
                                    numBytesRequested: 256 / 8));

            return hash;
        }
    }
}
