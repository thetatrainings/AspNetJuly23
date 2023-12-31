﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThetaEcommerce.Models;
using ThetaEcommerce.DTOs;

namespace ThetaEcommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly theta_ecommerceContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(theta_ecommerceContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //return _context.Products != null ? 
            //            View(await _context.Products.ToListAsync()) :
            //            Problem("Entity set 'theta_ecommerceContext.Products'  is null.");
            var ProductList = (from product in _context.Products
                               from cata in _context.Categories.Where(m => m.Id == product.CategoryId)
                               select new ProductModel
                               {
                                   Id = product.Id,
                                   Name = product.Name,
                                   CategoryId = cata.Id,
                                   CategoryName = cata.Name,
                                   Sku = product.Sku,
                                   Quantity = product.Quantity,
                                   Description = product.Description,
                               }).ToList();
            return View(ProductList);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.CatagoryList = _context.Categories.ToList();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Images,Quantity,Description,Price,Sku,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,SellerId,Status,CategoryId,Currency")] Product product, IList<IFormFile>? FileImages)
        {
            var CommmaSeperatedString = "";
            if(FileImages != null)
            {
                foreach (IFormFile item in FileImages)
                {
                    var ImagePath = "/images/" + Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);

                    using (FileStream dd = new FileStream(_webHostEnvironment.WebRootPath + ImagePath, FileMode.Create))
                    {
                        item.CopyTo(dd);
                    }
                    CommmaSeperatedString += "," + ImagePath;
                }
            }
            
            

            if (ModelState.IsValid)
            {
                if (CommmaSeperatedString.StartsWith(","))
                {
                    product.Images = CommmaSeperatedString.Remove(0, 1);
                }
                product.CreatedDate = DateTime.Now;
                product.CreatedBy = "System";
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Images,Quantity,Description,Price,Sku,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,SellerId,Status,CategoryId,Currency")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'theta_ecommerceContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            ViewBag.CatagoryList = _context.Categories.ToList();
            return View();
        }
        public string LoadProducts(int Id = 0)
        {
            var ProductsString = "";
            var ProductObject = _context.Products.Where(m => (Id > 0 ? m.CategoryId == Id : true)).Take(4).ToList();
            ProductsString += "<div class='card-deck'>";
            foreach (var product in ProductObject)
            {
                if(product.Images != null)
                {
                    string[] images = product.Images.Split(",");
                    ProductsString += "<div class='card'><img class='card-img-top' src='" + images[0] + "' alt='Card image cap'><div class='card-body'><h5 class='card-title'>" + product.Name + "</h5><p class='card-text'>" + product.Description + "</p></div></div>";
                }
                else
                {
                    ProductsString += "<div class='card'><img class='card-img-top' src='' alt='Card image cap'><div class='card-body'><h5 class='card-title'>" + product.Name + "</h5><p class='card-text'>" + product.Description + "</p></div></div>";
                }
            }
            return ProductsString + "</div>";
        }
    }
}
