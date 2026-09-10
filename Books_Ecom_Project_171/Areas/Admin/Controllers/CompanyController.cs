using Books_Ecom_Project_171_DataAccess.Repository.IRepository;
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
    }
}
