using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorLookupTool.Util
{
    public class LookupException : Exception
    {
        public LookupException(string message) : base(message) { }
    }
}