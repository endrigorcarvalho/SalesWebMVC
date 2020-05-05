using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? dataInicial, DateTime? dataFinal)
        {
            var result  = from obj in _context.SalesRecord select obj;
            if(dataInicial.HasValue)
            {
                result = result.Where(p => p.Date >= dataInicial);
            }
            if(dataFinal.HasValue)
            {
                result = result.Where(p => p.Date <= dataFinal);
            }

            return await result
                .Include(p => p.Seller)
                .Include(p => p.Seller.Department)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
        }
    }
}
