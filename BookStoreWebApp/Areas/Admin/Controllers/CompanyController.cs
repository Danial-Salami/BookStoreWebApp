using BookStore.DataAccess.Repository.IRepository;
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
    public class CompanyController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company
                .GetAll()
                .ToList();
           
            return View(objCompanyList);
        }
        public IActionResult Upsert(int? id)
        {
            Company company = new();
           
            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {
                Company companyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
               
              
                return View(companyFromDb);
            }
          
            
        }

        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {

                if (company.Id == 0 )
                {
                    _unitOfWork.Company.Add(company);
                    TempData["success"] = "Company successfuly created.";

                }
                else
                {
                _unitOfWork.Company.Update(company);
                    TempData["success"] = "Company successfuly updated.";

                }
                _unitOfWork.Save();
              
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(company);
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
            Company? companyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Company.Remove(companyFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Company successfuly Deleted.";
            return RedirectToAction("Index");


        }
        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company
                .GetAll()
                .ToList();
            return Json(new {data= objCompanyList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDelete = _unitOfWork.Company.Get(u=>u.Id == id);
            if (CompanyToBeDelete == null) 
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
         
            _unitOfWork.Company.Remove(CompanyToBeDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });


        }
        #endregion
    }

}
