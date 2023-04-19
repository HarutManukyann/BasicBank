using System.Security.Cryptography;
using System.Text;
using FCBankBasicHelper.Attributes;

namespace BL.Hashing
{
    public class Encryption
    {
        public void EncryptData(object data)
        {
            if (data == null) return;

            Type type = data.GetType();
            foreach (var property in type.GetProperties())
            {
                var attributes = property.GetCustomAttributes(typeof(PropertyAttribute), true);
                if (attributes.Length > 0)
                {
                    var value = property.GetValue(data).ToString();
                    if (value != null)
                    {
                        property.SetValue(data, Encrypt(value));
                    }
                }
            }
        }
        public void DecryptData(object data)
        {
            if (data == null) return;
            Type type = data.GetType();
            foreach (var property in type.GetProperties())
            {
                var attributes = property.GetCustomAttributes(typeof(PropertyAttribute), true);
                if (attributes.Length > 0)
                {
                    var value = property.GetValue(data).ToString();
                    if (value != null)
                    {
                        property.SetValue(data, Decrypt(value));
                    }
                }
            }
        }     
        public string Encrypt(string data, string key = "b14ca5898a4e4133bbce2ea2315a1916")
        {
            byte[] initializationVector = Encoding.ASCII.GetBytes("abcede0123456789");
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = initializationVector;
                var symmetricEncryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream
                        (memoryStream as Stream,symmetricEncryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream as Stream))
                        {
                            streamWriter.Write(data);
                        }
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }
        public string Decrypt(string cipherText, string key = "b14ca5898a4e4133bbce2ea2315a1916")
        {
            byte[] initializationVector = Encoding.ASCII.GetBytes("abcede0123456789");
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = initializationVector;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var memoryStream = new MemoryStream(buffer))
                {
                    using (var cryptoStream = new CryptoStream
                        (memoryStream as Stream,
                        decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream as Stream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
