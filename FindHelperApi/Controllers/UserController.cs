using FindHelperApi.Models;
using FindHelperApi.Models.DTO;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [Route("login")]
        public ActionResult<GETUserDTO> Login(LOGINUserDTO userDTO)
        {
            var userAuthenticated = _userService.Login(userDTO);
            return userAuthenticated;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Create(CREATEUserDTO userDTO)
        {
            if (!ModelState.IsValid)
                throw new Exception("Os dados inseridos estão em um formato incorreto.");//TODO: create custom exceptions for each field in model.

            var userCreated = await _userService.InsertAsync(userDTO);

            return CreatedAtAction(nameof(Create), new { id = userCreated.Id }, userCreated);
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
        public async Task<ActionResult<GETUserDTO>> GetByid(int id)
        {
            var user = await _userService.FindByIdAsync(id);
            return Ok(user);
        }

        #region (opcional)getByName
        //[HttpGet]
        //[Route("name")]
        //public ActionResult<List<User>> GetByName()
        //{
        //    var users = _userService.FindAllByName();
        //    return Ok(users);
        //}//TODO: fix function... implements how to get user by name with entity framework
        #endregion

    }
}
