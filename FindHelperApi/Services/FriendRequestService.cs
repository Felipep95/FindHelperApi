using FindHelperApi.Data;
using FindHelperApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHelperApi.Services
{
    public class FriendRequestService
    {
        private readonly FindHelperApiContext _context;

        public FriendRequestService(FindHelperApiContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(FriendRequest friendRequest)
        {
            _context.Add(friendRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FriendRequest>> FindAllAsync() => await _context.FriendRequests.ToListAsync();

        public async Task RemoveAsync(int id)
        {
            var friendRequest = await _context.FriendRequests.FindAsync(id);
            _context.Remove(friendRequest);
            await _context.SaveChangesAsync();
        }

    }
}
