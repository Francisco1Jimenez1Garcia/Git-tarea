using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Ventas.Application.ValueObjects;
using Ventas.Persistence.Model;

namespace Ventas.Application.Queries
{
    public class GetProductsQuery
    {
        private VentasContext productContext; 

        public GetProductsQuery()
        {
            var connectionString = "Server=.;Database=Ventas;Trusted_Connection=True;TrustServerCertificate=True;";
           
            var options = new DbContextOptionsBuilder<VentasContext>()
                .UseSqlServer(connectionString)
                .Options;

            productContext = new VentasContext(options);
        }   

        public List<Producto> Execute()
        {
            var productos = productContext.Products.Select( dbProd => new Producto
            {
                ProductId = dbProd.ProductId,               
                ProductName = dbProd.Name,
                CategoryId = dbProd.CategoryId,
                Price = dbProd.Price,
                Image  = dbProd.Image,
                CategoryName = dbProd.Category.Name

            }).ToList();

            return productos;

        }

    }
}
