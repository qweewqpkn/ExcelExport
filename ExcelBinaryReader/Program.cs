using BinaryConfig;
using System.Collections.Generic;


namespace ExcelBinaryReader
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryConfigManager.Instance.LoadAllBinaryData(@"E:\code\C#\ExcelExport\Output\ByteConfig\Config.byte");
            List<BattleSkill> list = BinaryConfigManager.Instance.LoadBinaryData<BattleSkill>("BattleSkill");
        }
    }
}
