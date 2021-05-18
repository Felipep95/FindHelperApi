using FindHelperApi.Models;
using FindHelperApi.Models.DTO;
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
        public async Task<ActionResult<Area>> Create(CREATEAreaDTO area)
        {
            if (!ModelState.IsValid)
                return Problem(statusCode: 400, title: "o dado inserido está em um formato incorreto");

            var createdArea = await _areaService.InsertAsync(area);

            return CreatedAtAction(nameof(Create), new { id = createdArea.Id }, createdArea);
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
