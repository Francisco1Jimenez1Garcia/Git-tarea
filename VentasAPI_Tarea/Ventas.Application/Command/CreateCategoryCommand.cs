using Microsoft.EntityFrameworkCore;
using Ventas.Application.ValueObjects;
using Ventas.Persistence.Model;

namespace Ventas.Application.Command
{
    public class CreateCategoryCommand
    {
        private readonly VentasContext _context;

        public CreateCategoryCommand()
        {
            var connectionString = "Server=.;Database=Ventas;Trusted_Connection=True;TrustServerCertificate=True;";

            var options = new DbContextOptionsBuilder<VentasContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new VentasContext(options);
        }

        public Categoria Execute(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.CategoryName))
            {
                throw new ArgumentException("El nombre de la categoría es obligatorio.");
            }

            var categoryEntity = new Category
            {
                Name = categoria.CategoryName.Trim()
            };

            _context.Categories.Add(categoryEntity);
            _context.SaveChanges();

            categoria.CategoryId = categoryEntity.CategoryId;
            return categoria;
        }
    }
}
