using ST10390916_PROG7311_POE.Data;
using ST10390916_PROG7311_POE.Models;

namespace ST10390916_PROG7311_POE.Services
{
    public class ProductService
    {

        public ProductService()
        {
            AppDBContext context = new AppDBContext();
            context.Database.EnsureCreated();
        }

        public void AddProduct(Product product)
        {
            AppDBContext context = new AppDBContext();

            if (product.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder); // Ensure folder exists

                string uniqueFileName = $"u{product.UserID}_{Guid.NewGuid()}{Path.GetExtension(product.ProductImage.FileName)}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProductImage.CopyTo(fileStream);
                }

                // Store relative path to the image
                product.ImagePath = $"/images/{uniqueFileName}";
            }

            context.Products.Add(product);
            context.SaveChanges();
        }

        public Product GetProductByID(int productID)
        {
            AppDBContext context = new AppDBContext();
            Product product = context.Products.FirstOrDefault(e => e.ProductID == productID);
            return product;
        }

        public List<Product> GetAllProducts()
        {
            AppDBContext context = new AppDBContext();
            List<Product> products = context.Products.ToList<Product>();
            return products;
        }

        public List<Product> GetProductsByUserID(int userID)
        {
            AppDBContext context = new AppDBContext();
            List<Product> products = context.Products.Where(e => e.UserID == userID).ToList<Product>();
            return products;
        }

        public void AddInitialProducts()
        {
            AppDBContext context = new AppDBContext();

            if (context.Products.ToList<Product>().Count < 1)
            {
                Product product = new Product();
                product.UserID = 3;
                product.ProductName = "Apple";
                product.Category = ProductCategory.Crops;
                product.Price = 10.00m;
                product.ProductionDate = DateOnly.FromDateTime(DateTime.Now);
                product.ImagePath = "/images/Apples.png";
                AddProduct(product);

                Product product1 = new Product();
                product1.UserID = 4;
                product1.ProductName = "Solar panel";
                product1.Category = ProductCategory.Crops;
                product1.Price = 10.00m;
                product1.ProductionDate = DateOnly.FromDateTime(DateTime.Now);
                product1.ImagePath = "/images/u10_0f4a3d93-3a42-44b1-acc0-bfd042044e9d.jpg";
                AddProduct(product1);

            }
        }

    }

    
}
