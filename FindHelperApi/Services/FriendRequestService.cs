using FindHelperApi.Data;
using FindHelperApi.Helper.CustomExceptions;
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

        public async Task<GETFriendRequestDTO> InsertAsync(CREATEFriendRequestDTO friendRequestDTO)
        {

            var requestExist = _context.FriendRequests
                       .Where(x => x.UserIdReceveidSolicitation == friendRequestDTO.UserIdReceveidSolicitation && x.UserIdSolicitation == friendRequestDTO.UserIdSolicitation)
                       .FirstOrDefault();

            if (requestExist?.Status == true)
            {
                requestExist.Status = false;
                _context.FriendRequests.Update(requestExist);
                await _context.SaveChangesAsync();

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
        }

        public async Task<GETFriendRequestDTO> FriendRequestResponse(CREATEFriendRequestDTO friendRequestDTO)
        {
            var userFriendRequest = _context.FriendRequests
                       .Where(x => x.UserIdReceveidSolicitation == friendRequestDTO.UserIdReceveidSolicitation && x.UserIdSolicitation == friendRequestDTO.UserIdSolicitation)
                       .FirstOrDefault();

            if (userFriendRequest == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Não há solicitação de amizade");
            }
            else if (userFriendRequest.Status == true)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                userFriendRequest.Status = true;
                userFriendRequest.isFriend = friendRequestDTO.IsFriend;
                 _context.FriendRequests.Update(userFriendRequest);
                await _context.SaveChangesAsync();

                var getFriendRequestDTO = new GETFriendRequestDTO();
                getFriendRequestDTO.Id = userFriendRequest.Id;
                getFriendRequestDTO.UserIdSolicitation = userFriendRequest.UserIdSolicitation;
                getFriendRequestDTO.UserIdReceveidSolicitation = userFriendRequest.UserIdReceveidSolicitation;
                getFriendRequestDTO.Status = userFriendRequest.Status;
                getFriendRequestDTO.IsFriend = userFriendRequest.isFriend;

                return getFriendRequestDTO;

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
