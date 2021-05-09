using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FindHelperApi.Models;
using FindHelperApi.Services;
using System;

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

        [HttpPost]
        [Route("register")]
        public ActionResult<User> Create(User user)
        {
            if (!ModelState.IsValid)
                throw new Exception();

             _userService.Insert(user);
            return StatusCode(201);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<User>> GetAll()
        {
            var users =  _userService.FindAll();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<User> GetByid(int id)
        {
            var user = _userService.FindById(id);
            
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        //TODO: create route GetByName
        [HttpGet]
        [Route("{name:string}")]
        public ActionResult<User> GetByName(string name)
        {
            var user = _userService.FindByName(name);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

    }
}
