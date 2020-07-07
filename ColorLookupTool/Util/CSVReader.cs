using System;
using System.IO;

namespace ColorLookupTool.Util
{
    public class CSVReader : IDisposable
    {
        private StringReader reader;

        public CSVReader(string csv, bool skipFirst = true)
        {
            reader = new StringReader(csv);
            if(skipFirst) reader.ReadLine();
        }

        public bool ReadRow(out string line)
        {
            line = reader.ReadLine();
            return reader.Peek() != -1;
        }

        public void Dispose()
        {
            reader.Close();
        }
    }
}
