using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poster.Core.Models
{
    public class CreatePostModel
    {
        public string Title { get; set; }

        public string Introdution { get; set; }

        public string MainPart { get; set; }

        public string Conclusion { get; set; }

        public long UserId { get; set; }

        public long PostCategoryId { get; set; }
    }
}
