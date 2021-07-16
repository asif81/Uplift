using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.DataAccess.Data.Repository;
using Uplift.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Uplift.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class WebImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WebImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? Id)
        {
            WebImages webImage = new WebImages();

            if (Id == null)
            {
                return View(webImage);
            }
            else
            {
                webImage = _unitOfWork.webImage.Get(Id.GetValueOrDefault());

                if (webImage == null)
                {
                    return NotFound();
                }
            }
            return View(webImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(int Id, WebImages webImage)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    webImage.Picture = p1;
                }

                if (webImage.Id == 0)
                {
                    _unitOfWork.webImage.Add(webImage);
                }
                else
                {
                    var webImageFromDb = _unitOfWork.webImage.Get(Id);
                    webImageFromDb.Name = webImage.Name;

                    if (files.Count > 0)
                    {
                        webImageFromDb.Picture = webImage.Picture;
                    }
                    _unitOfWork.webImage.Update(webImage);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(webImage);
        }

        #region "API calls"
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.webImage.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var objFromDb = _unitOfWork.webImage.Get(Id);

            if (objFromDb == null)
            {
                return Json(new { success = false, error = "Error deleting image!" });
            }

            _unitOfWork.webImage.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully!" });
        }

        #endregion
    }



}
