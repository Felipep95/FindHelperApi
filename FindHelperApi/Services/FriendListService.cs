using FindHelperApi.Data;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO.FriendListDTO;
using FindHelperApi.Models.DTO.FriendRequestDTO;
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

        public async Task<GETFriendListDTO> InsertAsync(CREATEFriendRequestDTO friendRequestDTO)
        {
            var newFriendList = new FriendList();

            newFriendList.UserId = friendRequestDTO.UserIdReceveidSolicitation;
            newFriendList.UserFriendId = friendRequestDTO.UserIdSolicitation;
            
            _context.FriendLists.Add(newFriendList);
            await _context.SaveChangesAsync();

            var getFriendList = new GETFriendListDTO();
            
            getFriendList.Id = newFriendList.Id;
            getFriendList.UserId = newFriendList.UserId;
            getFriendList.UserFriendId = newFriendList.UserFriendId;

            return getFriendList;

        }

        public async Task<List<FriendList>> FindAllAsync() => await _context.FriendLists.ToListAsync();
    }
}
