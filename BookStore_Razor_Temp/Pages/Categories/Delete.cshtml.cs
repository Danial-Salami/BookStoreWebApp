using BookStore_Razor_Temp.Data;
using BookStore_Razor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore_Razor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(int? id)
        {
            if (id == null || id == 0)
            {
                 NotFound();
            }
            Category = _dbContext.categories.Find(id);
            if (Category == null)
            {
                 NotFound();
            }
            
        }
        public IActionResult OnPost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category = _dbContext.categories.Find(id);
            if (Category == null)
            {
                return NotFound();
            }
            _dbContext.categories.Remove(Category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category successfuly deleted.";
            return RedirectToPage("Index");


        }
    }
}
