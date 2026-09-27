using Microsoft.EntityFrameworkCore;
using Ventas.Application.ValueObjects;
using Ventas.Persistence.Model;

namespace Ventas.Application.Command
{
    public class CreateProductCommand
    {
        private readonly VentasContext _context;

        public CreateProductCommand()
        {
            var connectionString = "Server=.;Database=Ventas;Trusted_Connection=True;TrustServerCertificate=True;";

            var options = new DbContextOptionsBuilder<VentasContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new VentasContext(options);
        }

        public Producto Execute(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.ProductName))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.");
            }

            var category = _context.Categories
                .FirstOrDefault(c => c.CategoryId == producto.CategoryId);

            if (category == null)
            {
                throw new ArgumentException("La categoría indicada no existe.");
            }

            if (producto.Price < 0)
            {
                throw new ArgumentException("El precio no puede ser negativo.");
            }

            var productEntity = new Product
            {
                Name = producto.ProductName.Trim(),
                CategoryId = producto.CategoryId,
                Price = producto.Price,
                Image = producto.Image
            };

            _context.Products.Add(productEntity);
            _context.SaveChanges();

            producto.ProductId = productEntity.ProductId;
            producto.ProductName = productEntity.Name;
            producto.CategoryName = category.Name;

            return producto;
        }
    }
}
