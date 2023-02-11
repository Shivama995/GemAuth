using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using Common.Configuration;
using Common.Extensions;

namespace Common.Cryptography.Implementations
{
    public class Crypt : ICrypt
    {
        private readonly string Key;
        private readonly byte[] iv;

        public Crypt(IConfigManager configManager)
        {
            Key = configManager.Get("CryptKey");
            iv = new byte[16];
        }

        public async Task<string> Encrypt<T>(T model)
        {
            string PlainText = model.Serialize();
            //string PlainText = JsonSerializer.Serialize(model);
            byte[] Array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(PlainText);
                        }
                        Array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(Array);
        }
        public async Task<T> Decrypt<T>(string PlainText)
        {
            byte[] buffer = Convert.FromBase64String(PlainText);
            string SerializedModel;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            SerializedModel = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            //return JsonSerializer.Deserialize<T>(SerializedModel);
            return SerializedModel.Deserialize<T>();
        }
    }
}
