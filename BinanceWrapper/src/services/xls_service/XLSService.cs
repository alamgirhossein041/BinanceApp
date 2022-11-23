using OfficeOpenXml;
using System.Runtime.CompilerServices;

namespace Services.XLSService
{
    public class XLSService : ServiceBase
    {
        public XLSService(IApp app) : base(app)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public override void StartService()
        {
            this.SaveXLSFile("binance_app");
        }

        private void AddContentToCell(int row, string column, string content)
        {

        }

        private void SaveXLSFile(string fileName)
        {
            FileInfo fileInfo = new FileInfo(String.Format(@"C:\BinanceApp\{0}.xls", fileName));
            if (fileInfo.Exists) {
                fileInfo.Delete();
            }

            using (ExcelPackage package = new ExcelPackage(fileInfo)) {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("binance_app");

                package.Save();
            }
        }
    }
}