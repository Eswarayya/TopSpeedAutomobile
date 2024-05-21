using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TopSpeed.Web.Data;
using TopSpeed.Web.Models;

namespace TopSpeed.Web.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _dbContext;                       // Get the data from the database end 

        private readonly IWebHostEnvironment _webHostEnvironment;              // Upload the image files 

        public BrandController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)                                               // Constructor
        {
            _dbContext = dbContext;

            _webHostEnvironment = webHostEnvironment;

        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Brand> brands = _dbContext.Brand.ToList();                // We do not initialize the list of the brands because get the data from the Database end



            return View(brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Brand brand)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;                 // Images are upload things 
            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)                                                    // Images are need to store on the below codes
            {
                string newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\brand");
                var extension = Path.GetExtension(file[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }

                brand.BrandLogo = @"\images\brand\" + newFileName + extension;
            }

            if (ModelState.IsValid)                                      // Validation for create the brands 
            {
                _dbContext.Brand.Add(brand);
                _dbContext.SaveChanges();

                TempData["success"] = "Record created successfully";        // To get the alert things 

                return RedirectToAction(nameof(Index));


            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(Guid id)                                   // To see the details things only
        
        { 
            Brand brand = _dbContext.Brand.FirstOrDefault(x => x.Id == id);

            return View(brand);
        
        }

        [HttpGet]
        public IActionResult Edit(Guid id)

        {
            Brand brand = _dbContext.Brand.FirstOrDefault(x => x.Id == id);

            return View(brand);

        }

        [HttpPost]
        public IActionResult Edit(Brand brand)                                   // To modify the data like edit or modify 
        {
            string webRootPath = _webHostEnvironment.WebRootPath;                 // Images are upload things 
            
            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)                                                    // Images are need to store on the below codes
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\brand");

                var extension = Path.GetExtension(file[0].FileName);

                //delete Old image 

                var objFromDb = _dbContext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);

                if (objFromDb.BrandLogo != null) 
                {
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }

                brand.BrandLogo = @"\images\brand\" + newFileName + extension;
            }

            if (ModelState.IsValid)
            {
                var objFromDb = _dbContext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);

                objFromDb.Name = brand.Name;
                objFromDb.EstablishedYear = brand.EstablishedYear;

                if(brand.BrandLogo != null)
                {
                    objFromDb.BrandLogo = brand.BrandLogo;
                }

                _dbContext.Brand.Update(objFromDb);
                _dbContext.SaveChanges();

                TempData["warning"] = "Record updated successfully";

                return RedirectToAction(nameof(Index));

            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid id)

        {
            Brand brand = _dbContext.Brand.FirstOrDefault(x => x.Id == id);

            return View(brand);

        }

        [HttpPost]

        public IActionResult Delete(Brand brand)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;                      // WWE root path details like image place

            if (string.IsNullOrEmpty(brand.BrandLogo))
            {
                //delete old image
                var objFromDb = _dbContext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);

                if (objFromDb.BrandLogo != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }
            _dbContext.Brand.Remove(brand);

            _dbContext.SaveChanges();


            TempData["error"] = "Record deleted successfully";

            return RedirectToAction(nameof(Index));

        }

    }
}
