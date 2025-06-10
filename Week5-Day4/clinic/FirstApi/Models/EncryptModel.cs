using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi.Models
{
        public class EncryptModel
        {
            public string? Data { get; set; }
            public byte[]? EncryptedData { get; set; }
            public byte[]? HashKey { get; set; }
        }
}