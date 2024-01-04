
using BookStore.DataAccess.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{
    public class CategoryController : Controller
    {   
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;

        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _dbContext.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {   
            if ( ModelState.IsValid ) {
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Category successfuly created.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null  || id == 0) {
                return NotFound();
            }
            Category? categoryFromDb = _dbContext.Categories.Find(id);
            if ( categoryFromDb == null )
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Category successfuly edited.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _dbContext.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? obj = _dbContext.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
                _dbContext.Categories.Remove(obj);
                _dbContext.SaveChanges();
            TempData["success"] = "Category successfuly deleted.";
            return RedirectToAction("Index");
            
           
        }
    }
}
