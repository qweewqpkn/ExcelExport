using ExcelDataReader;
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
        FileStream fs = File.Open(outputPath, FileMode.Create, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);

        DataRow nameRow = sheet.Rows[0];
        DataRow commentRow = sheet.Rows[1];
        DataRow typeRow = sheet.Rows[2];
        DataRow csRow = sheet.Rows[3];
        int rowNum = sheet.Rows.Count;
        int columnNum = GetRealColumns(sheet.Columns.Count, nameRow);

        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("local {0} = {1}\r\n", fileName, "{"); 

        for (int i = 4; i < rowNum; i++)
        {
            DataRow dataRow = sheet.Rows[i];

            sb.Append("{");
            for (int j = 0; j < columnNum; j++)
            {
                if (csRow[j].ToString().Contains("c"))
                {
                    sb.AppendFormat("{0} = {1},", nameRow[j], dataRow[j]);
                }
            }
            sb.Append("},\r\n");
        }

        sb.AppendLine("}");

        sw.Write(sb);

        sw.Close();
        fs.Close();
    }

    static int GetRealColumns(int columns, DataRow nameRow)
    {
        int realColumns = 0;
        for (int i = 0; i < columns; i++)
        {
            if (!string.IsNullOrEmpty(nameRow[i].ToString()))
            {
                realColumns++;
            }
        }

        return realColumns;
    }
}