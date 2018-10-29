using System.Collections.Generic;
using System.IO;

namespace BinaryConfig
{
    class FiledInfo
    {
        public int mInt;
        public float mFloat;
        public string mString;
        public bool mBool;
        public short mShort;
        public int[] mIntList;
        public float[] mFloatList;
        public string[] mStringList;
        public bool[] mBoolList;
        public Dictionary<int, int> mIntIntDic;
        public Dictionary<int, string> mIntStringDic;
        public Dictionary<string, int> mStringIntDic;
        public Dictionary<string, string> mStringStringDic;

        public int GetInt()
        {
            return mInt;
        }

        public float GetFloat()
        {
            return mFloat;
        }

        public string GetString()
        {
            return mString;
        }

        public bool GetBool()
        {
            return mBool;
        }

        public short GetShort()
        {
            return mShort;
        }

        public int[] GetIntList()
        {
            return mIntList;
        }

        public float[] GetFloatList()
        {
            return mFloatList;
        }

        public string[] GetStringList()
        {
            return mStringList;
        }

        public bool[] GetBoolList()
        {
            return mBoolList;
        }

        public Dictionary<int,int> GetIntIntDic()
        {
            return mIntIntDic;
        }

        public Dictionary<int, string> GetIntStringDic()
        {
            return mIntStringDic;
        }
        public Dictionary<string, int> GetStringIntDic()
        {
            return mStringIntDic;
        }
        public Dictionary<string, string> GetStringStringDic()
        {
            return mStringStringDic;
        }
    }

    class BinaryConfigReader
    {
        public enum DataType
        {
            eError = -1,
            eInt,
            eFloat,
            eString,
            eBool,
            eShort,
            eIntList,
            eFloatList,
            eStringList,
            eBoolList,
            eIntIntDic,
            eIntStringDic,
            eStringIntDic,
            eStringStringDic,
        }

        public List<BinaryConfigRow> mBinaryRowList = new List<BinaryConfigRow>();
        private BinaryReader mBinaryReader;
        private int mRowNum;
        private int mColumnNum;

        public BinaryConfigReader(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            mBinaryReader = new BinaryReader(ms);
            LoadData();
        }

        private void LoadData()
        {
            mRowNum = mBinaryReader.ReadInt32();
            mColumnNum = mBinaryReader.ReadInt32();

            List<string> nameList = new List<string>();
            for (int i = 0; i < mColumnNum; i++)
            {
                nameList.Add(mBinaryReader.ReadString());
            }

            List<int> typeList = new List<int>();
            for (int i = 0; i < mColumnNum; i++)
            {
                typeList.Add(mBinaryReader.ReadInt32());
            }

            mBinaryRowList.Clear();
            for (int i = 0; i < mRowNum; i++)
            {
                BinaryConfigRow row = new BinaryConfigRow();
                mBinaryRowList.Add(row);
                for (int j = 0; j < mColumnNum; j++)
                {
                    DataType type = (DataType)typeList[j];
                    FiledInfo fi = new FiledInfo();
                    row.mFiledInfoList.Add(fi);
                    switch (type)
                    {
                        case DataType.eInt:
                            {
                                int value = mBinaryReader.ReadInt32();
                                fi.mInt = value;
                            }
                            break;
                        case DataType.eFloat:
                            {
                                float value = mBinaryReader.ReadSingle();
                                fi.mFloat = value;
                            }
                            break;
                        case DataType.eString:
                            {
                                string value = mBinaryReader.ReadString();
                                fi.mString = value;
                            }
                            break;
                        case DataType.eBool:
                            {
                                int value = mBinaryReader.ReadInt32();
                                if(value == 0)
                                {
                                    fi.mBool = false;
                                }
                                else
                                {
                                    fi.mBool = true;
                                }
                            }
                            break;
                        case DataType.eShort:
                            {
                                short value = mBinaryReader.ReadInt16();
                                fi.mShort = value;
                            }
                            break;
                        case DataType.eIntList:
                            {
                                int length = mBinaryReader.ReadInt32();
                                int[] valueList = new int[length];                
                                for(int k = 0; k < length; k++)
                                {
                                    valueList[k] = mBinaryReader.ReadInt32();
                                }
                                fi.mIntList = valueList;
                            }
                            break;
                        case DataType.eFloatList:
                            {
                                int length = mBinaryReader.ReadInt32();
                                float[] valueList = new float[length];
                                for (int k = 0; k < length; k++)
                                {
                                    valueList[k] = mBinaryReader.ReadSingle();
                                }
                                fi.mFloatList = valueList;
                            }
                            break;
                        case DataType.eStringList:
                            {
                                int length = mBinaryReader.ReadInt32();
                                string[] valueList = new string[length];
                                for (int k = 0; k < length; k++)
                                {
                                    valueList[k] = mBinaryReader.ReadString();
                                }
                                fi.mStringList = valueList;
                            }
                            break;
                        case DataType.eBoolList:
                            {
                                int length = mBinaryReader.ReadInt32();
                                bool[] valueList = new bool[length];
                                for (int k = 0; k < length; k++)
                                {
                                    int value = mBinaryReader.ReadInt32();
                                    if (value == 0)
                                    {
                                        valueList[k] = false;
                                    }
                                    else
                                    {
                                        valueList[k] = true;
                                    }
                                }
                                fi.mBoolList = valueList;
                            }
                            break;
                        case DataType.eIntIntDic:
                            {
                                int length = mBinaryReader.ReadInt32();
                                Dictionary<int, int> dic = new Dictionary<int, int>();
                                for(int k = 0; k < length; k++)
                                {
                                    dic[mBinaryReader.ReadInt32()] = mBinaryReader.ReadInt32();
                                }
                                fi.mIntIntDic = dic;
                            }
                            break;
                        case DataType.eIntStringDic:
                            {
                                int length = mBinaryReader.ReadInt32();
                                Dictionary<int, string> dic = new Dictionary<int, string>();
                                for (int k = 0; k < length; k++)
                                {
                                    dic[mBinaryReader.ReadInt32()] = mBinaryReader.ReadString();
                                }
                                fi.mIntStringDic = dic;
                            }
                            break;
                        case DataType.eStringIntDic:
                            {
                                int length = mBinaryReader.ReadInt32();
                                Dictionary<string, int> dic = new Dictionary<string, int>();
                                for (int k = 0; k < length; k++)
                                {
                                    dic[mBinaryReader.ReadString()] = mBinaryReader.ReadInt32();
                                }
                                fi.mStringIntDic = dic;
                            }
                            break;
                        case DataType.eStringStringDic:
                            {
                                int length = mBinaryReader.ReadInt32();
                                Dictionary<string, string> dic = new Dictionary<string, string>();
                                for (int k = 0; k < length; k++)
                                {
                                    dic[mBinaryReader.ReadString()] = mBinaryReader.ReadString();
                                }
                                fi.mStringStringDic = dic;
                            }
                            break;
                    }
                }
            }
        }
    }

}

