using docuShare.Models.DTOs;

namespace docuShare.Interfaces
{
    public interface IDocumentService
    {
        public Task<string> GetDocumentContent(string fielname);
        public Task<IEnumerable<Models.Document>> GetAllDocuments();
        public Task<string> AddFile(FileRequestDto fileRequestDto);
        public Task<Models.Document> GetDocumentByFilename(string filename);
        public Task<IEnumerable<DocumentViewResponseDto>> GetAllDocumentsForView();
        
    }
}