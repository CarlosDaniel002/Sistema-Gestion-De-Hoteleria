using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.User
{
    public class GetPassword
    {
        // Genera el Hash con el string y el salting
        public static string GenerateHash(string Password, byte[] Salting)
        {
            byte[] combinedBytes, hashBytes, passwordBytes;
            string hashString;

            using (SHA512 sha512 = SHA512.Create())
            {
                passwordBytes = Encoding.UTF8.GetBytes(Password);
                combinedBytes = new byte[passwordBytes.Length + Salting.Length];
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(Salting, 0, combinedBytes, passwordBytes.Length-1, Salting.Length);
                hashBytes = sha512.ComputeHash(combinedBytes);
                hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            return hashString;
        }

        //  Crea un salting ramdon 
        public static byte[] GenerateSalting()
        {
            const int longitudSalt = 32;
            string saltingString;

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[longitudSalt];
                rng.GetBytes(salt);
                saltingString = BitConverter.ToString(salt).Replace("-", "").ToLower();
                return salt;
            }
        }

        // Convierte String en Byte.
        public static byte[] StringToByteArray(string dato)
        {
            int numberChars = dato.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(dato.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
