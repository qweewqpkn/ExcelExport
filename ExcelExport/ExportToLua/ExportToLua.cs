﻿using ExcelDataReader;
using System.Data;
using System.IO;
using System.Text;

class ExportToLua
{
    //excel的路径
    private static string mExcelPath;
    //输出的代码的路径
    private static string mCodePath;

    public static void Export()
    {
        FileStream configFS = File.Open("Config.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        StreamReader sr = new StreamReader(configFS);
        mExcelPath = sr.ReadLine();
        for(int i = 0; i < 3; i++)
        {
            mCodePath = sr.ReadLine();
        }

        //所有excel配置
        string[] allExcelPath = Directory.GetFiles(mExcelPath);
        for (int i = 0; i < allExcelPath.Length; i++)
        {
            string extensionName = Path.GetExtension(allExcelPath[i]);
            if (extensionName == ".xlsx")
            {
                FileStream excelFS = File.Open(allExcelPath[i], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(excelFS);
                DataSet book = reader.AsDataSet();
                ExportExcelToLua(book.Tables[0], allExcelPath[i]);
            }
        }
    }


    private static void ExportExcelToLua(DataTable sheet, string path)
    {
        string fileName = Path.GetFileNameWithoutExtension(path);
        string outputPath = mCodePath + fileName + ".lua";
        FileStream fs = File.Open(outputPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);

        StreamReader sr = new StreamReader(fs);
        string content = sr.ReadToEnd();
        string startFlag = "--@start 自动导出,请勿修改";
        string endFlag = "--@end";
        int endIndex = content.IndexOf(endFlag);
        string endContent = "";
        if (endIndex != -1)
        {
            endContent = content.Substring(endIndex + endFlag.Length + 2);
        }
        fs.Seek(0, SeekOrigin.Begin);
        fs.SetLength(0);

        DataRow nameRow = sheet.Rows[0];
        DataRow commentRow = sheet.Rows[1];
        DataRow typeRow = sheet.Rows[2];
        DataRow csRow = sheet.Rows[3];
        int rowNum = sheet.Rows.Count;
        int columnNum = Utility.GetRealColumns(sheet.Columns.Count, nameRow);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine(startFlag);
        sb.AppendFormat("local {0} = {1}\r\n", fileName, "{"); 

        for (int i = 4; i < rowNum; i++)
        {
            DataRow dataRow = sheet.Rows[i];

            sb.Append("{");
            for (int j = 0; j < columnNum; j++)
            {
                if (csRow[j].ToString().Contains("c"))
                {
                    string strValue = dataRow[j].ToString();
                    DataType type = Utility.SwitchTypeToEnumType(typeRow[j].ToString());
                    switch (type)
                    {
                        case DataType.eIntList:
                        case DataType.eBoolList:
                        case DataType.eFloatList:
                            {
                                sb.AppendFormat("{0} = {1}{2}{3}, ", nameRow[j], "{", strValue, "}");
                            }
                            break;
                        case DataType.eStringList:
                            {
                                sb.AppendFormat("{0} = {1}", nameRow[j], "{");
                                if(!string.IsNullOrEmpty(strValue))
                                {
                                    string[] list = strValue.Split(',');
                                    for (int k = 0; k < list.Length; k++)
                                    {
                                        sb.AppendFormat("\"{0}\", ", list[k]);
                                    }
                                }

                                sb.Append("},");
                            }
                            break;
                        case DataType.eStringIntDic:
                            {
                                sb.AppendFormat("{0} = {1}", nameRow[j], "{");
                                if (!string.IsNullOrEmpty(strValue))
                                {
                                    string[] list = strValue.Split(',');
                                    for (int k = 0; k < list.Length; k++)
                                    {
                                        string[] keyValues = list[k].Split(':');
                                        if (keyValues.Length == 2)
                                        {
                                            sb.AppendFormat("{0} = {1}, ", keyValues[0], keyValues[1]);
                                        }
                                    }
                                }
                                sb.Append("}, ");
                            }
                            break;
                        case DataType.eStringStringDic:
                            {
                                sb.AppendFormat("{0} = {1}", nameRow[j], "{");
                                if (!string.IsNullOrEmpty(strValue))
                                {
                                    string[] list = strValue.Split(',');
                                    for (int k = 0; k < list.Length; k++)
                                    {
                                        string[] keyValues = list[k].Split(':');
                                        if (keyValues.Length == 2)
                                        {
                                            sb.AppendFormat("{0} = \"{1}\", ", keyValues[0], keyValues[1]);
                                        }
                                    }
                                }
                                sb.Append("}, ");
                            }
                            break;
                        default:
                            {
                                sb.AppendFormat("{0} = {1}, ", nameRow[j], dataRow[j]);
                            }
                            break;
                    }
                }
            }
            sb.Append("},\r\n");
        }

        sb.AppendLine("}");
        sb.AppendLine(endFlag);
        sb.Append(endContent);
        sw.Write(sb);

        sw.Close();
        fs.Close();
    }
}