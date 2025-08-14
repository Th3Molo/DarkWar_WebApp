using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Mvc;

namespace DarkWar_WebApp.Pages
{
    public class GoogleDocs
    {
        #region Properties
        private readonly SheetsService _service;
        private readonly string _spreadsheetId = "1irDR_eJKuYYLHAJ1FXBw81JSV2Sr_LHqLxnkbVvbxP0";
        #endregion

        #region Constructor
        public GoogleDocs(IConfiguration config)
        {
            var credential = GoogleCredential
                .FromFile(config["Google:ServiceAccountKeyPath"])
                .CreateScoped(SheetsService.Scope.Spreadsheets);

            _service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "RazorPages Sheets Example",
            });
        }
        #endregion

        #region Methods
        public IList<IList<object>> GetData(string range)
        {
            var request = _service.Spreadsheets.Values.Get(_spreadsheetId, range);
            var response = request.Execute();
            return response.Values;
        }

        public void UpdateCell(string range, IList<IList<object>> values)
        {
            var valueRange = new ValueRange { Values = values };
            _service.Spreadsheets.Values.Update(valueRange, _spreadsheetId, range)
                .Execute();
        }
        #endregion
    }
}
