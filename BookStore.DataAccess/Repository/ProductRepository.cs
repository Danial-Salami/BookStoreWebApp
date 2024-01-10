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
 
        public class ProductRepository : Repository<Product>, IProductRepository
        {
            private ApplicationDbContext _dbContext;
            public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
            {
                _dbContext = dbContext;
            }


            public void Update(Product obj)
            {
                var objFromDb = _dbContext.Products.FirstOrDefault(u => u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Description = obj.Description;
                objFromDb.Author = obj.Author;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.ListPrice= obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.Price50= obj.Price50;
                objFromDb.Price100= obj.Price100;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl= obj.ImageUrl;
                }
                





            }
            }
        }
    }

