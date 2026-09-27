using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using Ventas.Application.ValueObjects;
using Ventas.Persistence.Model;

namespace Ventas.Application.Queries
{
    public class GetCategoriesQuery
    {
        private VentasContext categoriesContext;
        public GetCategoriesQuery()
        {
            var connectionString = "Server=.;Database=Ventas;Trusted_Connection=True;TrustServerCertificate=True;";

            var options = new DbContextOptionsBuilder<VentasContext>()
                .UseSqlServer(connectionString)
                .Options;

            categoriesContext = new VentasContext(options);
        }

        public List<Categoria> Execute()
        {
            var categorias = categoriesContext.Categories.Select(dbCat => new Categoria
            {
                CategoryId = dbCat.CategoryId,
                CategoryName = dbCat.Name

            }).ToList();

            return categorias;

        }

    }
}
