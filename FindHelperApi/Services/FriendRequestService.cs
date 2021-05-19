using FindHelperApi.Data;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO.FriendRequestDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace FindHelperApi.Services
{
    public class FriendRequestService
    {
        private readonly FindHelperApiContext _context;

        public FriendRequestService(FindHelperApiContext context)
        {
            _context = context;
        }

        public async Task<GETFriendRequestDTO> InsertAsync(CREATEFriendRequestDTO friendRequestDTO)
        {
            var createdFriendRequest = new FriendRequest();

            if (friendRequestDTO.Status == true)
            {
                createdFriendRequest.UserIdSolicitation = friendRequestDTO.UserIdSolicitation;
                createdFriendRequest.UserIdReceveidSolicitation = friendRequestDTO.UserIdReceveidSolicitation;
                createdFriendRequest.Status = friendRequestDTO.Status;

                _context.Add(createdFriendRequest);
                await _context.SaveChangesAsync();

                var getCreatedFriendRequest = new GETFriendRequestDTO();

                getCreatedFriendRequest.Id = createdFriendRequest.Id;
                getCreatedFriendRequest.UserIdSolicitation = createdFriendRequest.UserIdSolicitation;
                getCreatedFriendRequest.UserIdReceveidSolicitation = createdFriendRequest.UserIdReceveidSolicitation;
                getCreatedFriendRequest.Status = createdFriendRequest.Status;

                return getCreatedFriendRequest;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NoContent); //https://docs.microsoft.com/en-us/aspnet/web-api/overview/error-handling/exception-handling
            }
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
