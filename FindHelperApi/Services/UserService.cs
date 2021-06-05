using FindHelperApi.Data;
using FindHelperApi.Helper.CustomExceptions;
using FindHelperApi.Helper.ValidateDataAnotations;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using BC = BCrypt.Net.BCrypt;


//http://www.macoratti.net/13/12/c_vdda.htm

namespace FindHelperApi.Services
{
    public class UserService
    {
        private readonly FindHelperApiContext _context;

        public UserService(FindHelperApiContext context)
        {
            _context = context;
        }

        public async Task<GETUserDTO> Login(LOGINUserDTO userDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userDTO.Email);

            var isValidUser = user != null ? BC.Verify(userDTO.Password, user.Password) : throw new HttpStatusException(HttpStatusCode.BadRequest, "Erro: Email ou senha incorreto(a)");

            if (!isValidUser)
                throw new HttpStatusException(HttpStatusCode.BadRequest, "Erro: Email ou senha incorreto(a)");

            var getUserDto = new GETUserDTO();
            getUserDto.Id = user.Id;
            getUserDto.Name = user.Name;
            getUserDto.Email = user.Email;

            return getUserDto;
        }

        public async Task<List<GETUserDTO>> FindAllAsync()
        {
            var users = await _context.Users.ToListAsync();

            var listGetUser = new List<GETUserDTO>();

            for (int i = 0; i < users.Count(); i++)
            {
                var newGetuserDto = new GETUserDTO();

                newGetuserDto.Id = users[i].Id;
                newGetuserDto.Name = users[i].Name;
                newGetuserDto.Email = users[i].Email;

                listGetUser.Add(newGetuserDto);
            }

            return listGetUser;
        }

        public async Task<GETUserDTO> FindByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new HttpStatusException(HttpStatusCode.NotFound, "Usuário não encontrado");

            var getUserDto = new GETUserDTO();

            getUserDto.Id = user.Id;
            getUserDto.Name = user.Name;
            getUserDto.Email = user.Email;

            return getUserDto;
        }

        public async Task<GETUserDTO> InsertAsync(CREATEUserDTO userDTO)//TODO: insert custom exception to invalid ModelState also create functions to check if unique data exists in database, for example telephone,email...
        {
            var user = new User();

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Password = BC.HashPassword(userDTO.Password);

            //await ValidateUser(user);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var getUserDto = new GETUserDTO();

            getUserDto.Id = user.Id;
            getUserDto.Name = user.Name;
            getUserDto.Email = user.Email;

            return getUserDto;
        }

        #region opcional
        //public async Task UpdateAsync(User user)
        //{
        //    if (!_context.Users.Any(u => u.Id == user.Id))
        //        throw new KeyNotFoundException();//criar erro personalizado

        //    try
        //    {
        //        _context.Users.Update(user);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw; //criar erro personalizado
        //    }
        //}
        #endregion

        //    public async Task FindAllByName() => await _context.Users.Select(u => u.Name).ToListAsync();
        //    {
        //       return _context.Users.FromSqlRaw("SELECT Name FROM Users ORDER BY Name").ToListAsync();
        //}

        //public GETUserDTO GetByName(string name)
        //{
        //    //TODO
        //}

        //public async Task RemoveAsync(int id)
        //{
        //    var obj = await _context.Users.FindAsync(id);
        //    _context.Users.Remove(obj);
        //    await _context.SaveChangesAsync();
        //}


        // function to custom validate errors message from data annotations in models
        //private async Task ValidateUser(object obj, HttpContext httpContext)
        //{
        //    var errors = Validate.getValidationErros(obj);
        //    var responseError = "";

        //    foreach (var error in errors)
        //    {
        //         responseError = error.ErrorMessage;
        //    }

        //    var errorJson = JsonSerializer.Serialize(responseError);
        //    await httpContext.Response.WriteAsync(errorJson);
        //}
    }
}

