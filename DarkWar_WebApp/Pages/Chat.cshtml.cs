using DarkWar_WebApp.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DarkWar_WebApp.Pages
{
    public class ChatModel : PageModel
    {
        private readonly AppDbContext _context;

        public ChatModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Content { get; set; }

        public List<Message> Messages { get; set; } = new();

        public async Task OnGetAsync()
        {
            if (_context.Messages != null)
                Messages = await _context.Messages
                                         .OrderByDescending(m => m.Timestamp)
                                         .Take(50)
                                         .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Content))
            {
                var msg = new Message
                {
                    Username = Username,
                    Content = Content,
                    Timestamp = DateTime.Now
                };

                _context.Messages.Add(msg);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
