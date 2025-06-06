using docuShare.Interfaces;
using docuShare.Models.DTOs;
using docuShare.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace docuShare.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IAuthorizationService _authorizationService;

        public DocumentController(IDocumentService documentService,
                                  IAuthorizationService authorizationService)
        {
            _documentService = documentService;
            _authorizationService = authorizationService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<DocumentViewResponseDto>>> GetAllDocuments()
        {
            try
            {
                var documents = await _documentService.GetAllDocumentsForView();
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AccessLevelPolicy")]
        [HttpGet]
        public async Task<ActionResult<string>> GetDocument(string filename)
        {
            try
            {
                var doc = await _documentService.GetDocumentByFilename(filename);
                if (doc == null)
                    return NotFound();

                var authResult = await _authorizationService.AuthorizeAsync(User, null, new AccessLevelRequirement(doc.AccessLevel));
                if (!authResult.Succeeded)
                    return Forbid();
                var content = await _documentService.GetDocumentContent(filename);
                return Ok(content);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize (Roles = "Admin, HR")]
        public async Task<ActionResult<string>> AddFile([FromForm] FileRequestDto fileRequestDto)
        {
            if (fileRequestDto == null || fileRequestDto.File == null)
            {
                return BadRequest("File data cannot be null");
            }
            try
            {
                var status = await _documentService.AddFile(fileRequestDto);
                return Ok(status);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}