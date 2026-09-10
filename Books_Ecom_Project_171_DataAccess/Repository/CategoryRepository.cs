using Books_Ecom_Project_171_DataAccess.Data;
using Books_Ecom_Project_171_DataAccess.Repository.IRepository;
using Books_Ecom_Project_171_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books_Ecom_Project_171_DataAccess.Repository
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
