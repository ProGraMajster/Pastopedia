using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pastopedia.Data
{
    [Serializable]
    public class Pasta
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; }
    }
}
