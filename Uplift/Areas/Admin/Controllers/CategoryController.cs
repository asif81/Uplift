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

namespace Uplift.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? Id)
        {
            Category category = new Category();
            
            if (Id == null)
            {
                return View(category);
            }

            category = _unitOfWork.category.Get(Id.GetValueOrDefault());

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
           if (ModelState.IsValid)
           {
                if (category.Id == 0)
                {
                    _unitOfWork.category.Add(category);
                }
                else
                {
                    _unitOfWork.category.Update(category);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
           }
             return View(category);
        }

        #region "API calls"
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.category.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var objFromDb = _unitOfWork.category.Get(Id);

            if (objFromDb == null)
            {
                return Json(new { success=false, error= "Error deleting category!" });
            }

            _unitOfWork.category.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully!" });
        }

        #endregion
    }



}
