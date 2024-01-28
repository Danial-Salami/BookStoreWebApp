using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Areas.Admin.Controllers
{
	public class OrderController(IUnitOfWork unitOfWork) : Controller
	{

		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public IActionResult Index()
		{
			return View();
		}
		#region API CALL
		[HttpGet]
		public IActionResult GetAll()
		{
			List<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader
				.GetAll(includeProperties: "ApplicationUser")
				.ToList();
			return Json(new { data = objOrderHeaders });
		}
	
		#endregion
	}
}
