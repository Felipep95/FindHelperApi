﻿using FindHelperApi.Models;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindHelperApi.Controllers
{
    
    [ApiController]
    [Route("v1/publication")]
    public class PublicationController : ControllerBase
    {
        private readonly PublicationService _publicationService;

        public PublicationController(PublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<Publication>> Create(Publication publication)
        {
            if (!ModelState.IsValid)
                throw new Exception();

            await _publicationService.InsertAsync(publication);

            return CreatedAtAction(nameof(Create), new { id = publication.Id }, publication);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Publication>>> GetAll()
        {
            var publications = await _publicationService.FindAllAsync();
            return Ok(publications);
        }
    }
}
