using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConfig
{
    class BinaryConfigRow
    {
        public List<FiledInfo> mFiledInfoList = new List<FiledInfo>();
        
        public FiledInfo GetFieldInfo(int index)
        {
            if(index < mFiledInfoList.Count)
            {
                return mFiledInfoList[index];
            }
            else
            {
                Console.WriteLine("index beyong FiledInfoList");
                return null;
            }
        }
    }
}
