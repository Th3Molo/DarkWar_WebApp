using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DarkWar_WebApp.Pages
{
    public class GoogleDocsModel : PageModel
    {
        private readonly GoogleDocs _sheets;

        public IList<IList<object>> SheetData { get; set; }

        public GoogleDocsModel(GoogleDocs sheets)
        {
            _sheets = sheets;
        }

        public void OnGet()
        {
            SheetData = _sheets.GetData("Tabelle1!A1:C10");
        }

        public IActionResult OnPostUpdate(string cell, string newValue)
        {
            _sheets.UpdateCell(cell, new List<IList<object>> { new List<object> { newValue } });
            return RedirectToPage();
        }
    }
}
