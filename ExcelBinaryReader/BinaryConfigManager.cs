using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryConfig
{
    interface IBinaryData
    {
        void Init(BinaryConfigRow row);
    }

    class BinaryConfigManager
    {
        private Dictionary<string, byte[]> mBinaryDataMap = new Dictionary<string, byte[]>();

        private static BinaryConfigManager mInstance;
        public static BinaryConfigManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new BinaryConfigManager();
                }

                return mInstance;
            }
        }

        public void LoadAllBinaryData(string path)
        {
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            while(fs.Position != fs.Length)
            {
                string name = br.ReadString();
                int length = br.ReadInt32();
                byte[] bytes = br.ReadBytes(length);
                mBinaryDataMap[name] = bytes;
            }
        }

        public List<T> LoadBinaryData<T>(string name) where T : IBinaryData, new()
        {
            List<T> list = new List<T>();
            if(mBinaryDataMap.ContainsKey(name))
            {
                BinaryConfigReader reader = new BinaryConfigReader(mBinaryDataMap[name]);
                for(int i = 0; i < reader.mBinaryRowList.Count; i++)
                {
                    BinaryConfigRow row = reader.mBinaryRowList[i];
                    T data = new T();
                    data.Init(row);
                    list.Add(data);
                }
            }
            else
            {
                Console.WriteLine("没有对应的二进制数据，请查证！");
            }

            return list;
        }
            
    }
}
