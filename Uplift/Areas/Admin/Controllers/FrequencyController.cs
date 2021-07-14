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
    public class FrequencyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FrequencyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? Id)
        {
            Frequency frequency = new Frequency();

            if (Id == null)
            {
                return View(frequency);
            }

            frequency = _unitOfWork.frequency.Get(Id.GetValueOrDefault());

            if (frequency == null)
            {
                return NotFound();
            }

            return View(frequency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Frequency frequency)
        {
            if (ModelState.IsValid)
            {
                if (frequency.Id == 0)
                {
                    _unitOfWork.frequency.Add(frequency);
                }
                else
                {
                    _unitOfWork.frequency.Update(frequency);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(frequency);
        }

        #region "API calls"
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.frequency.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var objFromDb = _unitOfWork.frequency.Get(Id);

            if (objFromDb == null)
            {
                return Json(new { success = false, error = "Error deleting frequency!" });
            }

            _unitOfWork.frequency.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully!" });
        }

        #endregion
    }
}
