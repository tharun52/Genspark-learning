using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace docuShare.Models.DTOs
{
    public class FileRequestDto
    {
        public IFormFile File { get; set; } = null!; // Required file to be uploaded
        public int AccessLevel { get; set; } = 0; // Default access level for the file
    }
}