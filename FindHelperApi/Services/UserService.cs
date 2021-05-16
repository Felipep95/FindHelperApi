using FindHelperApi.Data;
using FindHelperApi.Helper;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using BC = BCrypt.Net.BCrypt;


namespace FindHelperApi.Services
{
    public class UserService
    {
        private readonly FindHelperApiContext _context;

        public UserService(FindHelperApiContext context)
        {
            _context = context;
        }

        public GETUserDTO Login(LOGINUserDTO userDTO)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == userDTO.Email);
            bool isValidPassword = BC.Verify(userDTO.Password, user.Password);

            if (!isValidPassword)
                throw new Exceptions("email ou senha incorreto");

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

            var getUserDto = new GETUserDTO();

            getUserDto.Id = user.Id;
            getUserDto.Name = user.Name;
            getUserDto.Email = user.Email;

            return getUserDto;
        }

        public async Task<GETUserDTO> InsertAsync(CREATEUserDTO userDTO)
        {
            var user = new User();

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Password = BC.HashPassword(userDTO.Password);

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

        //public async Task FindAllByName() => await _context.Users.Select(u => u.Name).ToListAsync();
        //{
        //   return _context.Users.FromSqlRaw("SELECT Name FROM Users ORDER BY Name").ToListAsync();
        //}

        //public async Task RemoveAsync(int id)
        //{
        //    var obj = await _context.Users.FindAsync(id);
        //    _context.Users.Remove(obj);
        //    await _context.SaveChangesAsync();
        //}
    }
}
