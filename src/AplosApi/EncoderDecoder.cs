using System;
using System.Security.Cryptography;
using System.Text;

namespace AplosApi
{
    public class EncoderDecoder : IDisposable
    {
        private readonly RSACryptoServiceProvider _rsa;

        public EncoderDecoder(string privateKey)
        {
            _rsa = OpenSslDecoder.DecodePrivateKey(privateKey);
        }

        public string EncryptData(string value)
        {
            byte[] encryptedBytes;
            byte[] messageBytes = Encoding.UTF8.GetBytes(value);

            try
            {
                encryptedBytes = _rsa.Encrypt(messageBytes, false);
            }
            catch
            {
                throw new CryptographicException("Unable to encrypt data.");
            }

            return encryptedBytes.Length == 0 ? "" : Convert.ToBase64String(encryptedBytes);
        }

        public string DecryptEncryptedData(string data)
        {
            byte[] decryptedBytes;

            try
            {
                decryptedBytes = _rsa.Decrypt(Convert.FromBase64String(data), false);
            }
            catch
            {
                throw new CryptographicException("Unable to decrypt data.");
            }

            return decryptedBytes.Length == 0 ? "" : Encoding.UTF8.GetString(decryptedBytes);
        }

        public void Dispose()
        {
            _rsa.Dispose();
        }
    }
}