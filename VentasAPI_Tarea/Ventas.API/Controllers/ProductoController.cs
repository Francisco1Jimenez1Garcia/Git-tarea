﻿using Microsoft.AspNetCore.Mvc;
using Ventas.Application.Command;
using Ventas.Application.ValueObjects;
using Ventas.Application.Queries;

namespace Ventas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "products")]
        public List<Producto> ObtenerProductos()
        {
            GetProductsQuery getProductsQuery = new GetProductsQuery();
            return getProductsQuery.Execute();
        }

        [HttpPost]
        public IActionResult CrearProducto([FromBody] Producto producto)
        {
            try
            {
                var command = new CreateProductCommand();
                var productoCreado = command.Execute(producto);
                return Ok(productoCreado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
