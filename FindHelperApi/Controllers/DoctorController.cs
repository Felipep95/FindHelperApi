using FindHelperApi.Models;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FindHelperApi.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorController (DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create (Doctor doctor)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _doctorService.InsertAsync(doctor);

            return CreatedAtAction(nameof(Create), new { id = doctor.Id }, doctor);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<Doctor>> GetAll()
        {
            var doctors = await _doctorService.FindAllAsync();
            return Ok(doctors);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Doctor>> GetById(int id)
        {
            var doctor = await _doctorService.FindByIdAsync(id);

            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        
    }
}
