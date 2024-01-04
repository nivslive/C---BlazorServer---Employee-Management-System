using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Account {
    public class LoginModel : PageModel
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


    public class InputModel 
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;}


        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}
    }
}