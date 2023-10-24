using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using ThetaEcommerce.DTOs;
using ThetaEcommerce.Models;

namespace ThetaEcommerce.Controllers
{
    public class SellersController : Controller
    {
        private readonly theta_ecommerceContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //CookieOptions _options = new CookieOptions();
        public SellersController(theta_ecommerceContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            //_options = options;
        }

        // GET: Sellers
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction(nameof(Login));
            }
              return _context.Sellers != null ? 
                          View(await _context.Sellers.ToListAsync()) :
                          Problem("Entity set 'theta_ecommerceContext.Sellers'  is null.");
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoginsModel loginModel, IFormFile? file)
        {

            //var ImagePath = "/images/" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            //using (FileStream dd = new FileStream(_webHostEnvironment.WebRootPath + ImagePath, FileMode.Create))
            //{
            //    file.CopyTo(dd);
            //}
            SystemUser system = new SystemUser();
            system.UserName = loginModel.UserName;
            system.Password = loginModel.Password;
            system.Role = "Seller";

            _context.SystemUsers.Add(system);
            _context.SaveChanges();

            if (!string.IsNullOrEmpty(loginModel.Email))
            {
                MailMessage mail = new MailMessage();
                mail.Subject = "Welcome to Theta Solution";
                mail.Body = "Welcome Dear" + loginModel.Name + "your company name are" + loginModel.CompanyName + "Thanks for joining as";
                mail.To.Add(loginModel.Email);
                mail.From = new MailAddress("xyz@gmail.com", "Theta Solutions");

                //SmtpClient client = new SmtpClient();
                //client.Port = 465;
                //client.Host = "smtp.gmail.com";
                //client.Credentials = new NetworkCredential("hhdhdhddd", "hdhdhdhdhdh");
                //client.EnableSsl = true;
                //client.Send(mail);

                /// For Mail Trap
                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("83145f6b4b6f97", "a83d1b34573635"),
                    EnableSsl = true
                };
                client.Send(mail);
            }


            Seller seller = new Seller();
            seller.Name = loginModel.Name;
            //seller.Image = ImagePath;
            seller.Address = loginModel.Address;
            seller.SystemUserId = system.Id;

            if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNo,Address,Email,SellerId,JoiningDate,CompanyName,SystemUserId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Image,Status")] Seller seller)
        {
            if (id != seller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.Id))
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
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sellers == null)
            {
                return Problem("Entity set 'theta_ecommerceContext.Sellers'  is null.");
            }
            var seller = await _context.Sellers.FindAsync(id);
            if (seller != null)
            {
                _context.Sellers.Remove(seller);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
          return (_context.Sellers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpGet]
        public IActionResult Login()
        {
            var user = Request.Cookies["Username"];
            //var user = _httpContextAccessor.HttpContext.Request.Cookies["Username"];
            if (!string.IsNullOrEmpty(user))
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginsModel logins)
        {
            var login = _context.SystemUsers.Where(m => m.UserName == logins.UserName && m.Password == logins.Password).FirstOrDefault();
            if(login == null)
            {
                return View();
            }
            HttpContext.Session.SetString("Username", login.UserName);

            //Cookies add
            _httpContextAccessor.HttpContext.Response.Cookies.Append("Username", login.UserName);
            //_options.Expires = DateTime.Now.AddDays(2);

            if(login.Role == "Seller")
            {
                var sellerModel = _context.Sellers.Where(m => m.SystemUserId == login.Id).FirstOrDefault();
                HttpContext.Session.SetString("Name", sellerModel.Name);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("Username");
            return RedirectToAction(nameof(Login));
        }
    }
}
