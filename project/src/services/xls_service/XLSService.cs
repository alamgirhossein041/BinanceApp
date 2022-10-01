using Excel = Microsoft.Office.Interop.Excel;

namespace Services.XLSService
{
    public class XLSService : ServiceBase
    {
        private Excel.Application excelApp = null;
        private Excel.Workbook currentWorkBook = null;
        private Excel.Worksheet currentWorkSheet = null;

        public XLSService(IApp app) : base(app)
        {
        }

        public override void StartService()
        {
            this.CreateXLSFile();
        }

        private void CreateXLSFile()
        {
            this.excelApp = new Excel.Application();
            this.currentWorkBook = excelApp.Workbooks.Add();
            this.currentWorkSheet = (Excel.Worksheet)excelApp.ActiveSheet;
        }

        private void AddContentToCell(int row, string column, string content)
        {
            this.currentWorkSheet.Cells[row, column] = content;
        }

        public void SaveXLSFile(string fileName)
        {
            this.currentWorkBook.SaveAs($"{Directory.GetCurrentDirectory()}\\{fileName}", Excel.XlFileFormat.xlOpenXMLWorkbook);
        }
    }
}