using Microsoft.VisualBasic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Data;
using WeatherMVC.Models;

namespace WeatherMVC.DAL.Repositories
{
    public class WeatherRepository
    {
        List<Weather> Weathers = new List<Weather>();
        List<int> yearss = new List<int>();


        public WeatherRepository(int year, int month)
        {
            GetData(year, month);
        }
        public WeatherRepository() { }

        public void GetData(int year, int month)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (db.Weathers.ToList().Count != 0)

                {
                    Weathers = GetDataFromBD(year, month);
                }
                else
                {
                    var names = Directory.GetFiles(@"DAL\Excel\");
                    for (int i = 0; i < names.Length; i++)
                    {
                        GetDataFromExcel(string.Format(names[i]));
                    }
                    Weathers = GetDataFromBD(year, month);

                }

            }
        }
        public List<Weather> GetWeathers()
        {
            return Weathers;
        }
        public List<Weather> GetDataFromBD(int year, int month)
        {
            List<Weather> temp = new List<Weather>();
            using (ApplicationContext db = new ApplicationContext())
            {
                IQueryable<Weather> weathers = db.Weathers.Where(p => p.Year == year.ToString());
                /*weathers = weathers.Where(p => p.Year == year.ToString());*/
                weathers = weathers.Where(p => p.Month == month.ToString());
                weathers = weathers.OrderBy(p => p.Date).ThenBy(p => p.Time);

                temp = weathers.ToList();
                return temp;
            }
        }



        public void GetDataFromExcel(string filePath)
        {

            IWorkbook Workbook;
            DataTable table = new DataTable();

            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // XSSFWorkbook подходит для формата XLSX, HSSFWorkbook подходит для формата XLS
                    string fileExt = Path.GetExtension(filePath).ToLower();
                    if (fileExt == ".xls")
                    {
                        Workbook = new HSSFWorkbook(fileStream);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        Workbook = new XSSFWorkbook(fileStream);
                    }
                    else
                    {
                        Workbook = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                for (int she = 0; she < Workbook.NumberOfSheets; she++)
                {
                    // Находим на первом листе
                    ISheet sheet = Workbook.GetSheetAt(she);

                    // Первая строка - это строка заголовка

                    for (int i = 4; i <= sheet.LastRowNum; i++)
                    {

                        IRow headerRow = sheet.GetRow(i);
                        int cellCount = headerRow.LastCellNum;// Номер первой строки и столбца
                                                              // Общее количество столбцов
                        IRow row = sheet.GetRow(i);
                        DataRow dataRow = table.NewRow();

                        List<string> vs = new List<string>();

                        if (row != null)
                        {

                            for (int z = row.FirstCellNum; z < cellCount; z++)
                            {
                                if (row.GetCell(z) != null)
                                {
                                    vs.Add(GetCellValue(row.GetCell(z)));
                                }
                            }
                            if (vs.Count < 12) { vs.Add(""); }
                        }

                        Weather weather = new Weather(vs);
                        db.Weathers.Add(weather);

                    }

                }
                db.SaveChanges();

            }
        }
        public static void UpdateDataFromExcel(string filePath)
        {
            WeatherRepository w = new WeatherRepository();
            w.GetDataFromExcel(filePath);
        }
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }

            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);

                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }
    }
}
