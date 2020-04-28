using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            this._context = context;
        }

        public void Create(Seller seller)
        {
            seller.Department = _context.Department.First();
            _context.Add(seller);
            _context.SaveChanges();
        }
        public List<Seller> FindAll()
        {

            return _context.Seller.ToList();
            //return department.Sellers.ToList();
        }
    }
}
