using FindHelperApi.Data;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO;
using FindHelperApi.Models.DTO.FriendListDTO;
using FindHelperApi.Models.DTO.FriendRequestDTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task <GETFriendRequestDTO> InsertAsync(CREATEFriendRequestDTO friendRequestDTO)
        {

            var requestExist = _context.FriendRequests
                       .Where(x => x.UserIdReceveidSolicitation == friendRequestDTO.UserIdReceveidSolicitation && x.UserIdSolicitation == friendRequestDTO.UserIdSolicitation)
                       .FirstOrDefault<FriendRequest>();

            if (requestExist?.Status == true)
            {
                requestExist.Status = false;
                _context.FriendRequests.Update(requestExist);

                var createdFriendRequest = new GETFriendRequestDTO();

                createdFriendRequest.Id = requestExist.Id;
                createdFriendRequest.UserIdSolicitation = requestExist.UserIdSolicitation;
                createdFriendRequest.UserIdReceveidSolicitation = requestExist.UserIdReceveidSolicitation;
                createdFriendRequest.Status = requestExist.Status;

                return createdFriendRequest;
            }
            else if (requestExist?.Status == false)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                var newFriendRequest = new FriendRequest();
                newFriendRequest.UserIdReceveidSolicitation = friendRequestDTO.UserIdReceveidSolicitation;
                newFriendRequest.UserIdSolicitation = friendRequestDTO.UserIdSolicitation;
                newFriendRequest.Status = false;
                newFriendRequest.isFriend = false;

                _context.FriendRequests.Add(newFriendRequest);
                await _context.SaveChangesAsync();

                var createdFriendRequest = new GETFriendRequestDTO();

                createdFriendRequest.Id = newFriendRequest.Id;
                createdFriendRequest.UserIdSolicitation = newFriendRequest.UserIdSolicitation;
                createdFriendRequest.UserIdReceveidSolicitation = newFriendRequest.UserIdReceveidSolicitation;
                createdFriendRequest.Status = newFriendRequest.Status;

                return createdFriendRequest;
            }

            

            //if (createdFriendRequest.Status == false)
            //{
            //    _context.FriendRequests.Add(createdFriendRequest);
            //    await _context.SaveChangesAsync();
            //}
            //else
            //{
            //    //var userFriendListAndRequest = new CREATEFriendListAndFriendRequestDTO();
            //    var addToFriendList = new FriendList();

            //    addToFriendList.UserFriendId = createdFriendRequest.UserIdReceveidSolicitation;
            //    addToFriendList.UserId = createdFriendRequest.UserIdSolicitation; 

            //    _context.FriendLists.Add(addToFriendList);
            //    await _context.SaveChangesAsync();

            //    _context.FriendRequests.Remove(createdFriendRequest);
                
            //}

            //_context.Add(createdFriendRequest);
            //await _context.SaveChangesAsync();

            //var getCreatedFriendRequest = new GETFriendRequestDTO();

            //getCreatedFriendRequest.Id = createdFriendRequest.Id;
            //getCreatedFriendRequest.UserIdSolicitation = createdFriendRequest.UserIdSolicitation;
            //getCreatedFriendRequest.UserIdReceveidSolicitation = createdFriendRequest.UserIdReceveidSolicitation;
            //getCreatedFriendRequest.Status = createdFriendRequest.Status;

            //return getCreatedFriendRequest;

            //if (friendRequestDTO.Status == true)
            //{
               
            //}
            //else
            //{
            //    throw new HttpResponseException(HttpStatusCode.NoContent); //https://docs.microsoft.com/en-us/aspnet/web-api/overview/error-handling/exception-handling
            //}
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
