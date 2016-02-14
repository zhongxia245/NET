using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;
using System.Data;
namespace CollectApp.common
{
    public class ExcelHelper
    {
        private string outFileName = "";
        private string fullFilename = "";
        private Workbook book = null;
        private Worksheet sheet = null;

        /// <summary>
        /// 导出（参数有两个）
        /// </summary>
        /// <param name="outfilename">存放路径</param>
        /// <param name="tempfilename">模板文件</param>
        public ExcelHelper(string outfilename, string tempfilename) //导出构造数
        {
            //处理文件的格式
            //outFileName = outfilename.Trim().Replace("进口:", "进口_").Replace(" ", "").Replace("&nbsp;", "").Replace("、", "");
            this.outFileName = outfilename;
            this.book = new Workbook();
            // book.Open(tempfilename);这里我们暂时不用模板
            this.sheet = book.Worksheets[0];
        }

        /// <summary>
        /// 导入的构造函数（参数只有一个）
        /// </summary>
        /// <param name="fullfilename">文件路径</param>
        public ExcelHelper(string fullfilename)
        {
            fullFilename = fullfilename;
        }

        /// <summary>
        /// 添加标题(未使用)
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="columnCount">列数</param>
        private void AddTitle(string title, int columnCount)
        {
            sheet.Cells.Merge(0, 0, 1, columnCount);
            sheet.Cells.Merge(1, 0, 1, columnCount);

            Cell cell1 = sheet.Cells[0, 0];
            cell1.PutValue(title);

            Style style = new Style();
            cell1.SetStyle(style);
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.Font.Name = "黑体";
            style.Font.Size = 14;
            style.Font.IsBold = true;

            Cell cell2 = sheet.Cells[1, 0];

            cell1.PutValue("查询时间：" + DateTime.Now.ToLocalTime());
            cell2.SetStyle(cell1.GetStyle());
        }

        /// <summary>
        /// 添加Excel头部
        /// </summary>
        /// <param name="dt"></param>
        private void AddHeader(DataTable dt,Worksheet sheetNew = null)
        {
            if (sheetNew == null) {
                sheetNew = sheet;
            }
            Cell cell = null;
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                cell = sheetNew.Cells[0, col];
                cell.PutValue(dt.Columns[col].ColumnName);

                Style style = new Style();
                style.Font.IsBold = true;
                cell.SetStyle(style);
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="dt"></param>
        private void AddBody(DataTable dt, Worksheet sheetNew = null)
        {
            if (sheetNew == null)
            {
                sheetNew = sheet;
            }
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    sheetNew.Cells[row + 1, col].PutValue(dt.Rows[row][col].ToString());
                }
            }
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dt">数据DataSet</param>
        /// <returns>Boolean</returns>
        public string DataSetToExcel(DataSet ds)
        {
            try
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTable dt = ds.Tables[i];
                    if (i >= book.Worksheets.Count) {
                        book.Worksheets.Add("sheet" + i);
                    }
                    Worksheet sheetNew = book.Worksheets[i];

                    sheetNew.Name = ds.Tables[i].TableName;
                    AddHeader(dt, sheetNew);
                    AddBody(dt, sheetNew);
                    sheet.AutoFitColumns();
                    sheet.AutoFitRows();
                }
                book.Save(outFileName);
                return "";
            }
            catch (Exception e)
            {
                Log.Write(e.StackTrace);
                Log.Write(e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dt">数据DataTable</param>
        /// <returns>正确返回空字符串</returns>
        public string DatatableToExcel(DataTable dt, String name = "sheet1")
        {
            try
            {
                sheet.Name = name;
                AddHeader(dt);
                AddBody(dt);
                sheet.AutoFitColumns();
                sheet.AutoFitRows();
                book.Save(outFileName);
                return "";
            }
            catch (Exception e)
            {
                Log.Write(e.StackTrace);
                Log.Write(e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <returns></returns>
        public DataTable ExcelToDatatalbe()
        {
            //打开Excel
            Workbook book = new Workbook(fullFilename);
            Worksheet sheet = book.Worksheets[0];
            Cells cells = sheet.Cells;

            //获取excel中的数据保存到一个datatable中
            DataTable dt_Import = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);
            return dt_Import;
        }
    }
}