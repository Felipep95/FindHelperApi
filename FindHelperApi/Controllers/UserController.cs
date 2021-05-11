using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FindHelperApi.Models;
using FindHelperApi.Services;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FindHelperApi.Controllers
{
    [ApiController]
    [Route("v1/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        //[HttpPost]
        //[Route("login")]
        //public ActionResult<User> Login(User user)
        //{
        //    var userAuthenticate = _userService.Login(user);
        //    return user;
        //}

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Create(User user)
        {
            if (!ModelState.IsValid)
                throw new Exception();

             await _userService.InsertAsync(user);

            return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users =  await _userService.FindAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> GetByid(int id)
        {
            var user = await _userService.FindByIdAsync(id);
            
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        //TODO: create route GetByName
        [HttpGet]
        [Route("{name}")]
        public ActionResult<User> GetByName(string name)
        {
            var user = _userService.FindByName(name);

            if (user == null)
                return NotFound();

            return Ok(user);
        }//TODO: fix function... implements how to get user by name with entity framework

    }
}
