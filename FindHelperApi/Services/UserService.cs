using System;
using System.Collections.Generic;
using System.Linq;
using FindHelperApi.Data;
using FindHelperApi.Models;


namespace FindHelperApi.Services
{
    public class UserService
    {
        private readonly FindHelperApiContext _context;

        public UserService (FindHelperApiContext context)
        {
            _context = context;
        }

        public List<User> FindAll() => _context.Users.ToList();

        public User FindById(int id) => _context.Users.Find(id);

        public User FindByName(string name) => _context.Users.Find(name);//TODO: implements get by name

        public void Insert(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            if (!_context.Users.Any(u => u.Id == user.Id))
                throw new KeyNotFoundException();

            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw ;
            }
        }

        public void Remove(int id)
        {
            var obj = _context.Users.Find(id);
            _context.Users.Remove(obj);
            _context.SaveChanges();
        }
    }
}
