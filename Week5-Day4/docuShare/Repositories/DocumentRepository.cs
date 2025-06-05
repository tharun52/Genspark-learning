
using docuShare.Contexts;
using docuShare.Models;
using FirstApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace docuShare.Repositories
{
    public class DocumentRepository : Repository<int, Document>
    {
        public DocumentRepository(DocumentContext documentContext) : base(documentContext)
        {
        }
        public override async Task<Document> Get(int key)
        {
            return await _documentContext.Documents.SingleOrDefaultAsync(d => d.Id == key);
        }

        public override async Task<IEnumerable<Document>> GetAll()
        {
            return await _documentContext.Documents.ToListAsync();
        }
    }
}