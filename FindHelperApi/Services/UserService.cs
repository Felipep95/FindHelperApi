using FindHelperApi.Data;
using FindHelperApi.Helper.CustomExceptions;
using FindHelperApi.Helper.StatusCode;
using FindHelperApi.Helper.Tests;
using FindHelperApi.Helper.Tests.StatusCodesExceptionsErrors;
using FindHelperApi.Helper.ValidateDataAnotations;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using WebApi;
using BC = BCrypt.Net.BCrypt;


//http://www.macoratti.net/13/12/c_vdda.htm

namespace FindHelperApi.Services
{
    public class UserService : ControllerBase
    {
        private readonly FindHelperApiContext _context;
        //private readonly ModelState _modelState;
        //private readonly ModelStateDictionary _modelStateDictionary;
        //private readonly ValidationActionFilter _validationActionFilter;

        public UserService(FindHelperApiContext context /*, ValidationActionFilter validationActionFilter, ModelState modelState, ModelStateDictionary modelStateDictionary*/)
        {
            _context = context;
            //_validationActionFilter = validationActionFilter;
            //_modelState = modelState;
            //_modelStateDictionary = modelStateDictionary;
        }

        public async Task<GETUserDTO> Login(LOGINUserDTO userDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == userDTO.Email);

            var isValidUser = user != null ? BC.Verify(userDTO.Password, user.Password) : throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Erro: Email ou senha incorreto(a)");

            if (!isValidUser)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Erro: Email ou senha incorreto(a)");

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
                throw new StatusCode404("Usuário não encontrado");

            var getUserDto = new GETUserDTO();

            getUserDto.Id = user.Id;
            getUserDto.Name = user.Name;
            getUserDto.Email = user.Email;

            return getUserDto;
        }

        public async Task<GETUserDTO> InsertAsync(CREATEUserDTO userDTO)//TODO: create custom exception in case email already exist.
        {
            var user = new User();

            //try
            //{
            user.Name = userDTO.Name;
            user.Email = VerifyEmail(userDTO.Email);
            user.Password = BC.HashPassword(userDTO.Password);

            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));
                throw new StatusCode400(message);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    throw new HttpStatusCodeException(HttpStatusCode.NotFound, ex.Message);

            //    //HttpContext httpContext;
            //    //var response = httpContext.Response;
            //    //response.ContentType = "application/json";

            //    //var errorResponse = new
            //    //{
            //    //    message = ex.Message,
            //    //    statusCode = response.StatusCode
            //    //};

            //    //var errorJson = JsonSerializer.Serialize(errorResponse);
            //    //await response.WriteAsync(errorJson);
            //}

            var getUserDto = new GETUserDTO();

            getUserDto.Id = user.Id;
            getUserDto.Name = user.Name;
            getUserDto.Email = user.Email;

            return getUserDto;
        }
        #region alternative getUserByName
        //public async Task<GETUserDTO> GetUserByName(string name)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);

        //    if (user == null)
        //        throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Usuário não encontrado");

        //    var getUserDTO = new GETUserDTO();

        //    getUserDTO.Id = user.Id;
        //    getUserDTO.Name = user.Name;
        //    getUserDTO.Email = user.Email;

        //    return getUserDTO;
        //}
        #endregion

        //TODO: create static helper class to put aditional functions to verify data in database.
        //https://www.pragimtech.com/blog/blazor/rest-api-model-validation/
        public string VerifyEmail(string email)//verify if email already exist on database.
        {
            var userEmailList = from u in _context.Users select u.Email;
            return userEmailList.Contains(email) ? throw new StatusCode400("O email informado já está sendo utilizado.") : email;
        }

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


