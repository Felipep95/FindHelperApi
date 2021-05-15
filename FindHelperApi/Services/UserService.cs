using FindHelperApi.Data;
using FindHelperApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<User>> FindAllAsync() => await _context.Users.ToListAsync();

        public async Task<User> FindByIdAsync(int id) => await _context.Users.FindAsync(id);

        //public async Task FindAllByName() => await _context.Users.Select(u => u.Name).ToListAsync();
        //{
        //   return _context.Users.FromSqlRaw("SELECT Name FROM Users ORDER BY Name").ToListAsync();
        //}

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
