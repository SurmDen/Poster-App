using Poster.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poster.Identity.Interfaces
{
    public interface ITokenService
    {
        public string GetToken(TokenModel tokenModel);

        public bool ValidateToken(string token);
    }
}
