using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Contexts;
using FirstApi.Interfaces;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Repositories
{
    public class FileRepository : Repository<int, Files>
    {
        public FileRepository(ClinicContext clinicContext) : base(clinicContext)
        {

        }

        public override Task<Files> Get(int key)
        {
            var file = _clinicContext.Files.SingleOrDefault(p => p.Id == key);
            return Task.FromResult(file ?? throw new Exception("No file with the given ID"));
        }

        public override async Task<IEnumerable<Files>> GetAll()
        {
            var files = _clinicContext.Files;
            if (!await files.AnyAsync())
                throw new Exception("No files in the database");
            return await files.ToListAsync();
        }
    }
}