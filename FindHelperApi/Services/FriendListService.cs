using FindHelperApi.Data;
using FindHelperApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHelperApi.Services
{
    public class FriendListService
    {
        private readonly FindHelperApiContext _context;

        public FriendListService(FindHelperApiContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(FriendList friendList)
        {
            _context.Add(friendList);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FriendList>> FindAllAsync() => await _context.FriendLists.ToListAsync();
    }
}
