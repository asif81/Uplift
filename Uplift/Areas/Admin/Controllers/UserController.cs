using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.Admin)]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return View(_unitOfWork.user.GetAll(u => u.Id != claims.Value));
        }

        public IActionResult Lock(string Id)
        {
            if (Id == null)
                return NotFound();

            _unitOfWork.user.LockUser(Id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UnLock(string Id)
        {
            if (Id == null)
                return NotFound();

            _unitOfWork.user.UnLockUser(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
