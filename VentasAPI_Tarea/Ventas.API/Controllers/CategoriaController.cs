using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ventas.Application.Command;
using Ventas.Application.Queries;
using Ventas.Application.ValueObjects;
using Ventas.Persistence.Model;

namespace Ventas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        

        [HttpGet(Name = "categories")]
        public List<Categoria> ObtenerCategorias()
        {
            GetCategoriesQuery getCategoriesQuery = new GetCategoriesQuery();
            return getCategoriesQuery.Execute();
        }

        [HttpPost]
        public IActionResult CrearCategoria([FromBody] Categoria categoria)
        {
            try
            {
                var command = new CreateCategoryCommand();
                var categoriaCreada = command.Execute(categoria);
                return Ok(categoriaCreada);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
