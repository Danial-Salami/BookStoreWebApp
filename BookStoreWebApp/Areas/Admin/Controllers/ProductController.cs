﻿using BookStore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using BookStore.Models.ViewModels;

using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreWebApp.Areas.Admin.Controllers
{ 
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class ProductController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product
                .GetAll(includeProperties:"Category")
                .ToList();
           
            return View(objProductList);
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category
                .GetAll()
                .Select(u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()
               }),
                Product = new Product()
               };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
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
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl
                            .TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                  
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.Product.Id == 0 )
                {
                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = "Product successfuly created.";

                }
                else
                {
                _unitOfWork.Product.Update(productVM.Product);
                    TempData["success"] = "Product successfuly updated.";

                }
                _unitOfWork.Save();
              
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll()
                    .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);  
            }     
        }
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product productFromDb = _unitOfWork.Product.Get(u=>u.Id==id);
        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
        //      .Select(u => new SelectListItem
        //      {
        //          Text = u.Name,
        //          Value = u.Id.ToString()
        //      });
        //    ViewBag.CategoryList = CategoryList;
        //    return View(productFromDb);
        //}
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
        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product
                .GetAll(includeProperties: "Category")
                .ToList();
            return Json(new {data=objProductList});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var productToBeDelete = _unitOfWork.Product.Get(u=>u.Id == id);
            if (productToBeDelete == null) 
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            if (productToBeDelete.ImageUrl != null)
            {
                var oldImagePath = Path.Combine(wwwRootPath, productToBeDelete.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.Product.Remove(productToBeDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });


        }
        #endregion
    }

}
