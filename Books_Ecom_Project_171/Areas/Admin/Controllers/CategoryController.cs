using Books_Ecom_Project_171_DataAccess.Repository.IRepository;
using Books_Ecom_Project_171_Models;
using Microsoft.AspNetCore.Mvc;

namespace Books_Ecom_Project_171.Areas.Admin.Controllers
{
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
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null) return View(category);
             category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (category == null) return BadRequest();
            if (!ModelState.IsValid) return View();
            if (category.Id == 0)
                _unitOfWork.Category.Add(category);
            else
                _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        #region APIs
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Category.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CategoryInDb = _unitOfWork.Category.Get(id);
            if (CategoryInDb == null)
                return Json(new { success = false, message = "Unable to Delete the data" });
            _unitOfWork.Category.Remove(CategoryInDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Data Deleted Successfully" });
        }
        #endregion
    }
}