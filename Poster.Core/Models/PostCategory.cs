using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poster.Core.Models
{
    public class PostCategory
    {
        public long Id { get; set; }

        public string CategoryName { get; set; }

        public List<Post> Posts { get; set; }
    }
}
