using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStoreWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    [Area("admin")]
	public class OrderController(IUnitOfWork unitOfWork) : Controller
	{

		private readonly IUnitOfWork _unitOfWork = unitOfWork;

		public IActionResult Index()
		{
			return View();
		}
        public IActionResult Details(int orderId)
        {
			OrderVM orderVM = new()
			{
				OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
				OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
			};
			
            return View(orderVM);
        }
        public IActionResult UpdateOrderDetails()
        {
            return View();
        }
        #region API CALL
        [HttpGet]
		public IActionResult GetAll(string status)
		{
			IEnumerable<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader
				.GetAll(includeProperties: "ApplicationUser")
				.ToList();
            switch (status)
            {
                case "inprocess":
                    objOrderHeaders = objOrderHeaders
						.Where(u=>u.OrderStatus==SD.StatusInProcess);
                    break;
                case "pending":
                    objOrderHeaders = objOrderHeaders
						.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment);
					break;
                case "completed":
                    objOrderHeaders = objOrderHeaders
						.Where(u => u.OrderStatus == SD.StatusShipped);
					break;
                case "approved":
                    objOrderHeaders = objOrderHeaders
						.Where(u => u.OrderStatus == SD.StatusApproved);
					break;
                default:
					 
                    break;
            }

            return Json(new { data = objOrderHeaders });
		}
	
		#endregion
	}
}
