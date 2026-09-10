using Books_Ecom_Project_171_DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books_Ecom_Project_171_DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category {  get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
