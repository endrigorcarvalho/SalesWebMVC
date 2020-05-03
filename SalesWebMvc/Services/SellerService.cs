using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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
            _context.Add(seller);
            _context.SaveChanges();
        }
        public List<Seller> FindAll()
        {

            return _context.Seller.ToList();
            //return department.Sellers.ToList();
        }

        public Seller FindById(int id)
        {

            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); // Include -> Fazer join com outra tabela
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if(!_context.Seller.Any(obj => obj.Id == seller.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbCurrencyException(e.Message);
            }
            
        }
    }
}
