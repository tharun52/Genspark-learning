using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi.Models.DTOs
{
    public class ErrorObjectDto
    {
        public int ErrorNumber { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}