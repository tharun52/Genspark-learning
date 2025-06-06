using docuShare.Models;

namespace docuShare.Interfaces
{
    public interface IEncryptionService
    {
        public Task<EncryptModel> EncryptData(EncryptModel data);
    }
}