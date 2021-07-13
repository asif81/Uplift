using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Extensions;
using Uplift.Models;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private HomeViewModel HomeVM;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM = new HomeViewModel()
            {
                CategoryList = _unitOfWork.category.GetAll(),
                ServiceList = _unitOfWork.service.GetAll(includeProperties: "Frequency")
            };
            return View(HomeVM);
        }

        public IActionResult Details(int Id)
        {
            var serviceFromDb = _unitOfWork.service.GetFirstOrDefault(includeProperties: "Category,Frequency", filter: c=>c.Id == Id);
            return View(serviceFromDb);
        }

        public IActionResult AddToCart(int serviceId)
        {
            List<int> SessionList = new List<int>();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionCart)))
            {
                SessionList.Add(serviceId);
                HttpContext.Session.SetObject(SD.SessionCart, SessionList);
            }
            else
            {
                SessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                if (!SessionList.Contains(serviceId))
                {
                    SessionList.Add(serviceId);
                    HttpContext.Session.SetObject(SD.SessionCart, SessionList);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
