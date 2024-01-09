﻿using BookStore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using BookStore.Models.ViewModels;

namespace BookStoreWebApp.Areas.Admin.Controllers
{ 
    [Area("Admin")]
    public class ProductController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
           
            return View(objProductList);
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()
               }),
                Product = new Product()
               };
            if (id == null || id == 0)
            {
                return View(productVM);
            } else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
               
              
                return View(productVM);
            }
          
            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product successfuly created.";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);  
            }     
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productFromDb = _unitOfWork.Product.Get(u=>u.Id==id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
              .Select(u => new SelectListItem
              {
                  Text = u.Name,
                  Value = u.Id.ToString()
              });
            ViewBag.CategoryList = CategoryList;
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(productFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Product successfuly Deleted.";
            return RedirectToAction("Index");


        }

    }

}
