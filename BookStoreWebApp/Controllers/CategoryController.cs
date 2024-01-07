
using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{
    public class CategoryController : Controller
    {   
       
        private readonly IUnitOfWork _unitOfWrok;
        public CategoryController(IUnitOfWork categoryRepository) 
        {
            _unitOfWrok = categoryRepository;


        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWrok.Category.GetAll().ToList();
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
                _unitOfWrok.Category.Add(obj);
                _unitOfWrok.Save();
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
            Category? categoryFromDb = _unitOfWrok.Category.Get(u => u.Id == id);
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
                _unitOfWrok.Category.Update(obj);
                _unitOfWrok.Save();
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
            Category? categoryFromDb = _unitOfWrok.Category.Get(u=>u.Id==id);
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
            Category? obj = _unitOfWrok.Category.Get(u=>u.Id==id);
            if (obj == null)
            {
                return NotFound();
            }
                _unitOfWrok.Category.Remove(obj);
                _unitOfWrok.Save();
            TempData["success"] = "Category successfuly deleted.";
            return RedirectToAction("Index");
            
           
        }
    }
}
