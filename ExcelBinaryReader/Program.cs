using BinaryConfig;
using System.Collections.Generic;


namespace ExcelBinaryReader
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryConfigManager.Instance.LoadAllBinaryData(@"E:\code\C#\ExcelExport\Output\ByteConfig\Config.byte");
            List<Achieve> list = BinaryConfigManager.Instance.LoadBinaryData<Achieve>("Achieve");
        }
    }
}
