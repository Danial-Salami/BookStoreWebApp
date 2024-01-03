using BookStore_Razor_Temp.Data;
using BookStore_Razor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore_Razor_Temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
          
                _dbContext.categories.Add(Category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category successfuly created.";
                return RedirectToPage("Index");
            
           
        }

    }
}
