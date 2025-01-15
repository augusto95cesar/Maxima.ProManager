using Microsoft.AspNetCore.Mvc;
using ProManager.Application.Mappers;
using ProManager.Application.Services;

namespace ProManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        { 
            return Ok(new DepartamentoService().GetALL().Map());
        }
    }
}
