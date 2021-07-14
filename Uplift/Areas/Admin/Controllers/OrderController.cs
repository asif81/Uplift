using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int Id)
        {
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                OrderHeder = _unitOfWork.orderHeader.Get(Id),
                OrderDetails = _unitOfWork.orderDetail.GetAll(filter:o=>o.OrderHeder.Id == Id)
            };
            
            return View(orderViewModel);
        }

        public IActionResult Approve(int Id)
        {

            var OrderHeder = _unitOfWork.orderHeader.Get(Id);
            if (OrderHeder == null)
                return NotFound();
            
            _unitOfWork.orderHeader.ChangeOrderHeader(Id, SD.StatusApproved);
            return View(nameof(Index));
        }

        public IActionResult Reject(int Id)
        {
            var OrderHeder = _unitOfWork.orderHeader.Get(Id);
            if (OrderHeder == null)
                return NotFound();

            _unitOfWork.orderHeader.ChangeOrderHeader(Id, SD.StatusRejected);
            return View(nameof(Index));
        }

        #region API Calls
        public IActionResult GetAllOrders()
        {
            return Json(new {data=_unitOfWork.orderHeader.GetAll() } );
        }
        public IActionResult GetAllPendingOrders()
        {
            return Json(new { data = _unitOfWork.orderHeader.GetAll(filter: o=>o.Status == SD.StatusSubmitted) });
        }
        public IActionResult GetAllApprovedOrders()
        {
            return Json(new { data = _unitOfWork.orderHeader.GetAll(filter: o => o.Status == SD.StatusApproved) });
        }
        #endregion
    }
}
