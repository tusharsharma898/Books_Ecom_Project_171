using Books_Ecom_Project_171_DataAccess.Data;
using Books_Ecom_Project_171_DataAccess.Repository.IRepository;
using Books_Ecom_Project_171_Models;
using Books_Ecom_Project_171_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books_Ecom_Project_171.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin+","+SD.Role_Employee)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null) return View(coverType);
            coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            if (coverType == null) return NotFound();
            return View(coverType);
        }
        [HttpPost]
        public IActionResult Upsert(CoverType coverType)
        {
            if(coverType == null) return BadRequest();
            if (!ModelState.IsValid) return View();
            if (coverType.Id == 0)
                _unitOfWork.CoverType.Add(coverType);
            else
                _unitOfWork.CoverType.Update(coverType);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        #region APIs
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.CoverType.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CoverInDb = _unitOfWork.CoverType.Get(id);
            if (CoverInDb == null)
                return Json(new { success = false, message = "Unable to Delete the data" });
            _unitOfWork.CoverType.Remove(CoverInDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "She has been deleted from your life... 😭💔😭🥀" });
        }
        #endregion
    }
}
