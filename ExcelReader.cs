using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;

namespace Konex.AutoEmailer
{
    public class ExcelReader
    {
        public string FilePath { private get; set; }

        private void ReadFile()
        {
            FilePath = @"C:\Users\Yini\Downloads\yini\datacom\Skills matrix and application knowledge (4 worksheets).xlsx";
            var extension = Path.GetExtension(@FilePath);
            
            FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read);
            
            IExcelDataReader excelReader = extension == ".xls" ? ExcelReaderFactory.CreateBinaryReader(stream) : ExcelReaderFactory.CreateOpenXmlReader(stream);

            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();

            while (excelReader.Read())
            {
                excelReader.GetInt32(0);
            }

            excelReader.Close();
        }

        public void AddExcelFileCommand()
        {
            ReadFile();
        }
    }
}
