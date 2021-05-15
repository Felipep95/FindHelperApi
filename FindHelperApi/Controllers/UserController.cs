﻿using FindHelperApi.Models;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHelperApi.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Create(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _userService.InsertAsync(user);

            return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _userService.FindAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetByid(int id)
        {
            var user = await _userService.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        //TODO: create route GetByName
        //[HttpGet]
        //[Route("name")]
        //public ActionResult<List<User>> GetByName()
        //{
        //    var users = _userService.FindAllByName();
        //    return Ok(users);
        //}//TODO: fix function... implements how to get user by name with entity framework

    }
}
