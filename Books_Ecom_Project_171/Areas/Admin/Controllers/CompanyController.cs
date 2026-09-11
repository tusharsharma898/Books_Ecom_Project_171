using Books_Ecom_Project_171_DataAccess.Repository.IRepository;
using Books_Ecom_Project_171_Models;
using Microsoft.AspNetCore.Mvc;

namespace Books_Ecom_Project_171.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null) return View(company);
            company = _unitOfWork.Company.Get(id.GetValueOrDefault());
            if (company == null) return NotFound();
            return View(company);
            ;
        }
        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (company == null) return BadRequest();
            if (!ModelState.IsValid) return View(company);
            if (company.Id == 0)
                _unitOfWork.Company.Add(company);
            else
                _unitOfWork.Company.Update(company);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        #region APIs
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Company.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CompanyInDb = _unitOfWork.Company.Get(id);
            if (CompanyInDb == null)
                return Json(new { success = false, message = "Not Able to delete" });
            _unitOfWork.Company.Remove(CompanyInDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Data Deleted successfully!!" });

        }
        #endregion
    }
}

