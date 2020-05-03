using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            
            return View(_sellerService.FindAll());
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]  // Para hacker não conseguir enviar códigos maliciosos
        public IActionResult Create(Seller seller)
        {
            _sellerService.Create(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { errorMessage = "Id not Providade" });
            }

            var seller = _sellerService.FindById(id.Value);
            if (seller ==  null)
            {
                return RedirectToAction(nameof(Error), new { errorMessage = "Id not Found" });
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { errorMessage = "Id not Providade"});
            }

            var seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { errorMessage = "Id not found" });
            }

            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { errorMessage = "Id not Providade" });
            }

            var seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { errorMessage = "Id not Found" });
            }

            List<Department> departments = _departmentService.FindAll();

            SellerFormViewModel viewModel = new SellerFormViewModel() { Departments = departments, Seller = seller };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Seller seller, int id)
        {
            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { errorMessage = "Id Mismatch" });
            }
            
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { errorMessage = e.Message });
            }
            catch(DbCurrencyException e )
            {
                return RedirectToAction(nameof(Error), new { errorMessage = e.Message });
            }


        }

        public IActionResult Error(string errorMessage)
        {
            var viewModel = new ErrorViewModel
            {
                Message = errorMessage,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}