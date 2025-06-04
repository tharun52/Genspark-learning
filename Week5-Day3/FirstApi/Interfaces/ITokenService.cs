using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Models;

namespace FirstApi.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
    }
}