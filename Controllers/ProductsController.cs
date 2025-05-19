using BestStoreMVC.Models;
using BestStoreMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestStoreMVC.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("/Admin/[controller]/{action=Index}/{id?}")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;
        private readonly int pageSize = 5;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult Index(int pageIndex, string? search, string? column, string? orderBy)
        {
            IQueryable<Product> query = context.Products;

            // search functionality
            if (search != null)
            {
                query = query.Where(p => p.Name.Contains(search) || p.Brand.Contains(search));
            }

            // sort functionality
            string[] validColumns = { "Id", "Name", "Brand", "Category", "Price", "CreatedAt" };
            string[] validOrderBy = { "desc", "asc" };

            if (!validColumns.Contains(column))
            {
                column = "Id";
            }

            if (!validOrderBy.Contains(orderBy))
            {
                orderBy = "desc";
            }

            if (column == "Name")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Name);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Name);
                }
            }
            else if (column == "Brand")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Brand);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Brand);
                }
            }
            else if (column == "Category")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Category);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Category);
                }
            }
            else if (column == "Price")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Price);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Price);
                }
            }
            else if (column == "CreatedAt")
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.CreatedAt);
                }
                else
                {
                    query = query.OrderByDescending(p => p.CreatedAt);
                }
            }
            else
            {
                if (orderBy == "asc")
                {
                    query = query.OrderBy(p => p.Id);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Id);
                }
            }

            //query = query.OrderByDescending(p => p.Id);

            //pagination functionality
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            decimal count = query.Count();
            int totalPages = (int)Math.Ceiling(count / pageSize);
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var products = query.ToList();

            ViewData["PageIndex"] = pageIndex;
            ViewData["TotalPages"] = totalPages;

            ViewData["Search"] = search ?? "";

            ViewData["Column"] = column;
            ViewData["OrderBy"] = orderBy;

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (productDto.ImageFile == null && productDto.Treeimage==null)
            {
                ModelState.AddModelError("ImageFile", "The image file is required");
            }

            if (!ModelState.IsValid)
            {
                return View(productDto);
            }


            // save the image file
           
           

            

          
          
           
            

                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string TreeFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                if (productDto.ImageFile == null || productDto.Treeimage == null)
                {
                    productDto.ImageFile = null;
                    productDto.Treeimage = null;
                }
                else
                {

                    newFileName += Path.GetExtension(productDto.ImageFile!.FileName);

                    string imageFullPath = environment.WebRootPath + "/products/" + newFileName;
                    using (var stream = System.IO.File.Create(imageFullPath))
                    {
                        productDto.ImageFile.CopyTo(stream);
                    }


                TreeFileName += Path.GetExtension(productDto.Treeimage!.FileName);


                string imagetreeFullPath = environment.WebRootPath + "/treeImage/" + TreeFileName;
                using (var stream = System.IO.File.Create(imagetreeFullPath))
                    {
                        productDto.Treeimage.CopyTo(stream);
                    }
                }

                // save the new product in the database
                Product product = new Product()
            {
                Name = productDto.Name,
                Brand = productDto.Brand,
                Category = productDto.Category,
                Price = productDto.Price,
                Description = productDto.Description,
                ImageFileName = newFileName,
                Treeimage= TreeFileName,
                CreatedAt = DateTime.Now,
            };


            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Index", "Products");
        }


        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            // create productDto from product
            var productDto = new ProductDto()
            {
                Name = product.Name,
                Brand = product.Brand,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description,
            };


            ViewData["ProductId"] = product.Id;
            ViewData["Treeimage"]=product.Treeimage;
            ViewData["ImageFileName"] = product.ImageFileName;
            ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");

            return View(productDto);
        }


        [HttpPost]
        public IActionResult Edit(int id, ProductDto productDto)
        {
            var product = context.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            // إذا الموديل غير صالح رجع الفورم مع بيانات العرض
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = product.Id;
                ViewData["Treeimage"] = product.Treeimage;
                ViewData["ImageFileName"] = product.ImageFileName;
                ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");

                return View(productDto);
            }

            string newFileName = product.ImageFileName;     // اسم الصورة الرئيسية (افتراضياً القديمة)
            string newImageTree = product.Treeimage;        // اسم صورة مخطط الشجرة (افتراضياً القديمة)

            // تحديث الصورة الرئيسية (ImageFile)
            if (productDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff")
                              + Path.GetExtension(productDto.ImageFile.FileName);

                string imageFullPath = Path.Combine(environment.WebRootPath, "products", newFileName);
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productDto.ImageFile.CopyTo(stream);
                }

                // حذف الصورة القديمة اذا موجودة
                string oldImageFullPath = Path.Combine(environment.WebRootPath, "products", product.ImageFileName);
                if (System.IO.File.Exists(oldImageFullPath))
                {
                    System.IO.File.Delete(oldImageFullPath);
                }
            }

            // تحديث صورة مخطط الشجرة (Treeimage)
            if (productDto.Treeimage != null)
            {
                newImageTree = DateTime.Now.ToString("yyyyMMddHHmmssfff")
                               + Path.GetExtension(productDto.Treeimage.FileName);

                string treeImageFullPath = Path.Combine(environment.WebRootPath, "TreeImage", newImageTree);
                using (var stream = System.IO.File.Create(treeImageFullPath))
                {
                    productDto.Treeimage.CopyTo(stream);
                }

                // حذف صورة مخطط الشجرة القديمة اذا موجودة
                string oldTreeImageFullPath = Path.Combine(environment.WebRootPath, "TreeImage", product.Treeimage);
                if (System.IO.File.Exists(oldTreeImageFullPath))
                {
                    System.IO.File.Delete(oldTreeImageFullPath);
                }
            }

            // تحديث بيانات المنتج مع الصور الجديدة أو القديمة حسب وجود تحديث
            product.Name = productDto.Name;
            product.Brand = productDto.Brand;
            product.Category = productDto.Category;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.ImageFileName = newFileName;
            product.Treeimage = newImageTree;

            context.SaveChanges();

            return RedirectToAction("Index", "Products");
        }

        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }
            string Treeimage = environment.WebRootPath + "/TreeImage/" + product.Treeimage;
            System.IO.File.Delete(Treeimage);

            string imageFullPath = environment.WebRootPath + "/products/" + product.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Products.Remove(product);
            context.SaveChanges(true);

            return RedirectToAction("Index", "Products");
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(int productId, IFormFile File, string Title, string Description)
        {
            // تحقق من أن ملف الصورة موجود
            if (File == null || File.Length == 0)
            {
                // إعادة التوجيه لصفحة التفاصيل مع رسالة خطأ مثلاً (اختياري)
                TempData["Error"] = "يرجى اختيار صورة صالحة للرفع.";
                return RedirectToAction("Tree", "Store", new { id = productId });
            }

            try
            {
                // تحديد مجلد الرفع داخل wwwroot/uploads
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                // إنشاء المجلد إذا لم يكن موجوداً
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // إنشاء اسم فريد للملف لتجنب تكرار الأسماء
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // حفظ الملف على القرص
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await File.CopyToAsync(stream);
                }

                // إنشاء كائن الصورة وربطه بالمنتج
                var image = new ImageModel
                {
                    Title = Title,
                    Description = Description,
                    Url = "/uploads/" + uniqueFileName,
                    CreatedAt = DateTime.Now,
                    ProductId = productId
                };

                // إضافة الصورة إلى قاعدة البيانات
                context.ImageModels.Add(image);
                await context.SaveChangesAsync();

                TempData["Success"] = "تم رفع الصورة بنجاح.";
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ (يمكنك إضافة لوج أو التعامل مع الخطأ بطريقة مناسبة)
                TempData["Error"] = "حدث خطأ أثناء رفع الصورة. حاول مرة أخرى.";
            }

            // إعادة التوجيه لصفحة التفاصيل بعد الانتهاء
            return RedirectToAction("Tree","Store", new { id = productId });
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id, int productId)
        {
            try
            {
                var image = await context.ImageModels.FindAsync(id);
                if (image == null)
                    return Json(new { success = false, message = "الصورة غير موجودة." });

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.Url.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                context.ImageModels.Remove(image);
                await context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ أثناء الحذف: " + ex.Message });
            }
        }

    }
}
