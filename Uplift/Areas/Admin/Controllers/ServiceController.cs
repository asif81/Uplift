using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.DataAccess.Data.Repository;
using Uplift.Models;
using Microsoft.AspNetCore.Hosting;
using Uplift.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Uplift.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        
        [BindProperty]
        public ServiceViewModel serviceVm { get; set; }
        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? Id)
        {
            ServiceViewModel serviceVm = new ServiceViewModel()
            {
                service = new Service(),
                frequecyList = _unitOfWork.frequency.GetFrequencyListFromDropDown(),
                categoryList = _unitOfWork.category.GetCategoryListFromDropDown()
            };

            if (Id != null)
            {
                serviceVm.service = _unitOfWork.service.Get(Id.GetValueOrDefault());
            }
           
            return View(serviceVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (serviceVm.service.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath,@"images\services");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads,fileName+extension),FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    serviceVm.service.ImageUrl = @"\images\services\" + fileName + extension;

                    _unitOfWork.service.Add(serviceVm.service);
                }
                else
                {
                    var serviceFromDb = _unitOfWork.service.Get(serviceVm.service.Id);
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\services");
                        var extension = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        
                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        serviceVm.service.ImageUrl = @"\images\services\" + fileName + extension;
                    }
                    else
                    {
                        serviceVm.service.ImageUrl = serviceFromDb.ImageUrl;
                    }

                    _unitOfWork.service.Update(serviceVm.service);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(serviceVm);
            }
        }

        #region "API calls"
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.service.GetAll(includeProperties: "Category,Frequency") });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var objFromDb = _unitOfWork.service.Get(Id);

            if (objFromDb == null)
            {
                return Json(new { success = false, error = "Error deleting service!" });
            }

            _unitOfWork.service.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully!" });
        }

        #endregion
    }
}
