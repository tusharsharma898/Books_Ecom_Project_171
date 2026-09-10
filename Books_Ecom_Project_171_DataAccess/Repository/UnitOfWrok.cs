using Books_Ecom_Project_171_DataAccess.Data;
using Books_Ecom_Project_171_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books_Ecom_Project_171_DataAccess.Repository
{
    public class UnitOfWrok:IUnitOfWork
    {
       private readonly ApplicationDbContext _context;
        public UnitOfWrok(ApplicationDbContext context)
        {
            _context = context;
            Category =  new CategoryRepository(context);
            CoverType=new CoverTypeRepository(context);
            Product = new ProductRepository(context);
            Company = new CompanyRepository(context);
            ApplicationUser= new ApplicationUserRepository(context);
        }

        public ICategoryRepository Category { get;private set;  }

        public ICoverTypeRepository CoverType {  get; private set; }

        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public void Save()
        {
            _context.ChangeTracker.Clear();
            _context.SaveChanges();
        }
    }
}
