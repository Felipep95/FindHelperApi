using FindHelperApi.Models;
using FindHelperApi.Models.DTO.DoctorDTO;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FindHelperApi.Controllers
{
    [Route("doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Doctor>> Create(CREATEDoctorDTO doctorDto)
        {
            if (!ModelState.IsValid || doctorDto == null)
                throw new Exception("Os dados inseridos estão inválidos");

            var createdDoctor = await _doctorService.InsertAsync(doctorDto);

            return CreatedAtAction(nameof(Create), new { id = createdDoctor.Id }, createdDoctor);
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
