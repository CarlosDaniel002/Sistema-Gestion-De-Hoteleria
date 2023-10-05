using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Usuario
{
    public class GetPassword
    {
        public static string GenerateHash(string Password, byte[] Salting)
        {
            byte[] combinedBytes, hashBytes, passwordBytes;
            string hashString;
            using (SHA512 sha512 = SHA512.Create())
            {
                passwordBytes = Encoding.UTF8.GetBytes(Password);
                combinedBytes = new byte[passwordBytes.Length + Salting.Length];
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(Salting, 0, combinedBytes, passwordBytes.Length, Salting.Length);
                hashBytes = sha512.ComputeHash(combinedBytes);
                // Generación del string.
                hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            return hashString;
        }

        public static byte[] GenerateSalting()
        {
            const int longitudSalt = 32;
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[longitudSalt];
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
}
