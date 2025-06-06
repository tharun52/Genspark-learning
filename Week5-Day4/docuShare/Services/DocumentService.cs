using docuShare.Interfaces;
using docuShare.Models;
using docuShare.Models.DTOs;
using Microsoft.AspNetCore.SignalR;
using docuShare.Misc;

namespace docuShare.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<int, Document> _documentRepository;
        private readonly IRepository<string, Role> _roleRepository;
        private readonly IHubContext<NotificationHub> _hubContext;

        public DocumentService(IRepository<int, Models.Document> documentRepository,
                               IRepository<string, Role> roleRepository,
                               IHubContext<NotificationHub> hubContext)
        {
            _documentRepository = documentRepository;
            _roleRepository = roleRepository;
                    _hubContext = hubContext;
        
        }
        public async Task<Models.Document> GetDocumentByFilename(string filename)
        {
            var documents = await _documentRepository.GetAll();
            return documents.FirstOrDefault(d => d.Filename == filename) ?? throw new Exception($"Document with filename {filename} not found.");
        }

        public async Task<IEnumerable<DocumentViewResponseDto>> GetAllDocumentsForView()
        {
            var documents = await _documentRepository.GetAll();
            return documents.Select(d => new DocumentViewResponseDto
            {
                Filename = d.Filename,
                FileFormat = d.FileFormat ?? string.Empty,
                AccessLevel = d.AccessLevel,
                CreatedAt = d.CreatedAt
            });
        }
        public async Task<string> GetDocumentContent(string filename)
        {

            var documents = await _documentRepository.GetAll();
            var doc = documents.FirstOrDefault(d => d.Filename == filename);
            if (doc == null)
            {
                throw new InvalidOperationException($"Document with filename {filename} not found.");
            }

            var textFormats = new[]
            {
                ".txt", ".csv", ".json", ".xml", ".html", ".md",
                ".cs", ".py", ".csproj", ".sln"
            };
            if (textFormats.Contains((doc.FileFormat ?? string.Empty).ToLower()))
            {
                return doc.Content != null ? System.Text.Encoding.UTF8.GetString(doc.Content) : string.Empty;
            }
            return Convert.ToBase64String(doc.Content ?? Array.Empty<byte>());
        }

        public async Task<IEnumerable<Models.Document>> GetAllDocuments()
        {
            return await _documentRepository.GetAll();
        }


        public async Task<string> AddFile(FileRequestDto fileRequestDto)
        {
            try
            {
                if (fileRequestDto.File.Length == 0)
                {
                    throw new InvalidOperationException("File is empty.");
                }

                var roles = await _roleRepository.GetAll();
                var maxAccessLevel = roles.Max(r => r.AccessLevel);
                if (fileRequestDto.AccessLevel <= 0 || fileRequestDto.AccessLevel > maxAccessLevel)
                {
                    throw new InvalidOperationException($"AccessLevel must be between 0 and {maxAccessLevel}.");
                }
                var document = new Document
                {
                    Filename = fileRequestDto.File.FileName,
                    StoragePath = Path.Combine("uploads", fileRequestDto.File.FileName),
                    FileFormat = Path.GetExtension(fileRequestDto.File.FileName),
                    Status = "Uploaded",
                    AccessLevel = fileRequestDto.AccessLevel
                };

                using (var memoryStream = new MemoryStream())
                {
                    await fileRequestDto.File.CopyToAsync(memoryStream);
                    await _hubContext.Clients.All.SendAsync("DocumentAdded", document.Filename);
                    document.Content = memoryStream.ToArray();
                }

                await _documentRepository.Add(document);
                return "File added successfully: " + document.Filename;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error adding file: {ex.Message}", ex);
            }
        }
    }
}