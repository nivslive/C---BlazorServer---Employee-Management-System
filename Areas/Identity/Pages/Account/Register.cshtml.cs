using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Account {
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public InputModel Input {get; set;}
        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPostAsync() 
        {
            return Page();
        }
    }
}