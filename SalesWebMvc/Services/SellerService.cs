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

        public async Task CreateAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Seller>> FindAllAsync()
        {

            return await _context.Seller.ToListAsync();
            //return department.Sellers.ToList();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {

            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); // Include -> Fazer join com outra tabela
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                throw new IntegrityException("Is impossible delete a seller with sales.");
            }
            
        }

        public async Task UpdateAsync(Seller seller)
        {
            if(! await _context.Seller.AnyAsync(obj => obj.Id == seller.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbCurrencyException(e.Message);
            }
            
        }
    }
}
