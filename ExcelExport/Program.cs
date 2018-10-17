using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace ExcelExport
{
    class Program
    {
        static void Main(string[] args)
        {
            ExportToCSharp.Export();
            ExportToLua.Export();
        }
    }
}
