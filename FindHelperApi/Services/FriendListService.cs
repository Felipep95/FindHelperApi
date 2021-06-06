using FindHelperApi.Data;
using FindHelperApi.Helper.CustomExceptions;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO.FriendListDTO;
using FindHelperApi.Models.DTO.FriendRequestDTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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
            var requestExist = _context.FriendRequests
                       .Where(x => x.UserIdReceveidSolicitation == friendRequestDTO.UserIdReceveidSolicitation && x.UserIdSolicitation == friendRequestDTO.UserIdSolicitation)
                       .FirstOrDefault();

            if (requestExist == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Não há solicitação de amizade");
            }
            else if (requestExist.Status == true && requestExist.isFriend == true)
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
            else if (requestExist.Status == true && requestExist.isFriend == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Solicitação de amizade recusada");
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Pedido de amizade pendente");
            }
        }

        public async Task<List<FriendList>> FindAllAsync() => await _context.FriendLists.ToListAsync();
    }
}
