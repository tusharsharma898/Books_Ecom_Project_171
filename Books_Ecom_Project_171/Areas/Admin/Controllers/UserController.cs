using Books_Ecom_Project_171_DataAccess.Data;
using Books_Ecom_Project_171_DataAccess.Repository.IRepository;
using Books_Ecom_Project_171_Models;
using Books_Ecom_Project_171_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books_Ecom_Project_171.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(ApplicationDbContext context,IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork=unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _context.ApplicationUsers.ToList();
            var roleList = _context.Roles.ToList();
            var userRoles = _context.UserRoles.ToList();
            foreach (var user in userList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roleList.FirstOrDefault(r => r.Id == roleId).Name;
                if (user.CompanyId == null)
                {
                    user.Company = new Company()
                    {
                        Name=""
                    };
                }
                if (user.CompanyId != null)
                {
                    user.Company = new Company()
                    {
                        Name = _unitOfWork.Company.Get(Convert.ToInt32(user.CompanyId)).Name
                    };
                }
            }
            var adminUser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            userList.Remove(adminUser);

            return Json(new { data = userList });
        }
        [HttpPost]
        public IActionResult LockUnLock([FromBody]string id)
        {
            bool isLocked = false;
            var userInDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if(userInDb == null)
            {
                return Json(new { success = false, message = "Error while locking/unlocking" });
            }
            if (userInDb != null && userInDb.LockoutEnd>DateTime.Now)
            {
                //user is currently locked, we will unlock them
                userInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userInDb.LockoutEnd = DateTime.Now.AddYears(1000);
                isLocked = true;
            }
            _context.SaveChanges();
            return Json(new { success = true, message = isLocked == true ? "User Successfully Locked!!":"User Successfully Unlocked!!" });
        }
        #endregion
    }
}
