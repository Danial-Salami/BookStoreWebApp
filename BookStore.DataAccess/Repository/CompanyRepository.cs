using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
 
        public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
            private ApplicationDbContext _dbContext;
            public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
            {
                _dbContext = dbContext;
            }


            public void Update(Company obj)
            {
                var objFromDb = _dbContext.Companies.FirstOrDefault(u => u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.City = obj.City;
                objFromDb.State = obj.State;
                objFromDb.StreetAddress = obj.StreetAddress;
                objFromDb.PostalCode = obj.PostalCode;
                objFromDb.PhoneNumber= obj.PhoneNumber;
             
            
                





            }
            }
        }
    }

