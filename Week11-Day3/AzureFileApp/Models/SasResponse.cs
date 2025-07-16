using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFileApp.Models
{
    public class SasResponse
    {
        public string sasUrl { get; set; }
        public DateTimeOffset expiresOn { get; set; }

    }
}