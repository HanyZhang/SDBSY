using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteToExcel(@"D:\1.xls");
            Console.WriteLine("ok");
            Console.ReadKey();
        }
        public static void WriteToExcel(string filePath)
        {
            using (Stream fileStream = File.OpenWrite(filePath))
            {
                IWorkbook wb = new HSSFWorkbook();//如果生成xlsx则是XSSFWorkbook
                ISheet sheet = wb.CreateSheet();
                //合并单元格区域 例： 第1行到第1行 第1列到第7列围成的矩形区域
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 6));
                IRow row = sheet.CreateRow(0);//0行号
                row.CreateCell(0).SetCellValue("rupeng");
                row.CreateCell(1).SetCellValue(3.14);
                wb.Write(fileStream);
            }

        }

    }
}
