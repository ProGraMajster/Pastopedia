using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pastopedia
{
    [Serializable]
    public class LogEventClass
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public string Level { get; set; }
        public string CodeLog { get; set; }
        public DateTime DateTime = DateTime.Now;
    }
}
