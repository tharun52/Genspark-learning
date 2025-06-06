using System.Security.Cryptography;
using System.Text;
using docuShare.Interfaces;
using docuShare.Models;


namespace docuShare.Services
{
    public class EncryptionService : IEncryptionService
    {
        public async Task<EncryptModel> EncryptData(EncryptModel data)
        {
            return await Task.Run(() =>
            {
                HMACSHA256 hMACSHA256;
                if (data.HashKey != null)
                    hMACSHA256 = new HMACSHA256(data.HashKey);
                else
                    hMACSHA256 = new HMACSHA256();
                data.EncryptedData = hMACSHA256.ComputeHash(Encoding.UTF8.GetBytes(data.Data ?? string.Empty));
                data.HashKey = hMACSHA256.Key;
                return data;
            });
        }
    }
}