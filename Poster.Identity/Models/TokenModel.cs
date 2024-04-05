using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poster.Identity.Models
{
    public class TokenModel
    {
        public string UserName { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;
    }
}
