namespace AvailabilityMonitor.Services.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Excel = Microsoft.Office.Interop.Excel;

    public class ExcelService : IExcelService
    {
        public void GenerateFile<T>(List<T> obj)
        {
            var dataTable = ConvertToDataTable(obj);
            GenerateExcel(dataTable, @"C:\Users\rspir\Downloads\modmasa.xlsx");
        }

        private void GenerateExcel(DataTable dataTable, string path)
        {
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);

            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Add();
            Excel._Worksheet xlWorksheet = (Excel._Worksheet)excelWorkBook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            foreach (DataTable table in dataSet.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name  
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                // add all the columns  
                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                // add all the rows  
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }

            excelWorkBook.SaveAs(path);
            excelWorkBook.Close();
            excelApp.Quit();
        }

        private DataTable ConvertToDataTable<T>(List<T> models)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            
            foreach (T item in models)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
