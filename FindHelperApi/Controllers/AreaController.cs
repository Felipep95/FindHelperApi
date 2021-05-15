using FindHelperApi.Models;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FindHelperApi.Controllers
{
    
    [ApiController]
    [Route("area")]
    public class AreaController : ControllerBase
    {
        private readonly AreaService _areaService;

        public AreaController(AreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Create(Area area)
        {
            if (!ModelState.IsValid)
                return Problem(statusCode: 400, title: "o dado inserido está em um formato incorreto");

            await _areaService.InsertAsync(area);

            return CreatedAtAction(nameof(Create), new { id = area.Id }, area);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<FriendList>> GetAll()
        {
            var areas = await _areaService.FindAllAsync();
            return Ok(areas);
        }
    }
}
