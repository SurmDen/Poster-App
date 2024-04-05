using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poster.Core.Models
{
    public class UserWithToken
    {
        public long UserId { get; set; }

        public string Token { get; set; }
    }
}
