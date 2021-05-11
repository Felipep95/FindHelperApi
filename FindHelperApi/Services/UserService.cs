using FindHelperApi.Data;
using FindHelperApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;


namespace FindHelperApi.Services
{
    public class UserService
    {
        private readonly FindHelperApiContext _context;

        public UserService (FindHelperApiContext context)
        {
            _context = context;
        }

        //public User Login(User user)
        //{
        //    var userAuthenticate = _context.Users.SingleOrDefault(u => u.Email == user.Email);
        //    bool isValidPassword = BC.Verify(user.Password, userAuthenticate.Password);

        //    if (isValidPassword)
        //        return user;

        //    return null;
        //}

        public async Task<List<User>> FindAllAsync() => await _context.Users.ToListAsync();
        
        public async Task<User> FindByIdAsync(int id) => await _context.Users.FindAsync(id);

        public User FindByName(string name) => _context.Users.Find(name);//TODO: implements get by name

        public async Task InsertAsync(User user)
        {
            user.Password = BC.HashPassword(user.Password);
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            if (!_context.Users.Any(u => u.Id == user.Id))
                throw new KeyNotFoundException();//criar erro personalizado

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw; //criar erro personalizado
            }
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Users.FindAsync(id);
            _context.Users.Remove(obj);
            await _context.SaveChangesAsync();
        }
    }
}
