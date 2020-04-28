using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            
            return View(_sellerService.FindAll());
        }

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]  // Para hacker não conseguir enviar códigos maliciosos
        public IActionResult Create(Seller seller)
        {
            _sellerService.Create(seller);
            return RedirectToAction(nameof(Index));
        }

    }
}