using BrunoZell.ModelBinding;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FindHelperApi.Controllers
{

    [ApiController]
    [Route("publication")]
    public class PublicationController : ControllerBase
    {
        private readonly PublicationService _publicationService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PublicationController(PublicationService publicationService, IWebHostEnvironment webHostEnvironment)
        {
            _publicationService = publicationService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateNewPublication([ModelBinder(BinderType = typeof(JsonModelBinder))] CreatePublicationDTO publicationDTO,
        IFormFile file)
        {
            if (!ModelState.IsValid || publicationDTO == null)
                return BadRequest();

            var publicationCreated = await _publicationService.SaveAsync(publicationDTO, file);
            return CreatedAtAction(nameof(CreateNewPublication), new { id = publicationCreated.Id }, publicationCreated);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Publication>>> GetAll()
        {
            var publications = await _publicationService.FindAllAsync();
            return Ok(publications);
        }

        //public string UploadedFile(PublicationDTO publication)
        //{
        //    string unicFileName = null;

        //    if (publication.Photo != null)
        //    {
        //        string filePhoto = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        //        unicFileName = Guid.NewGuid().ToString() + "_" + publication.Photo.FileName;
        //        string filePath = Path.Combine(filePhoto, unicFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            publication.Photo.CopyTo(fileStream);
        //        }
        //    }

        //    return unicFileName;
        //}
    }
}
