using FindHelperApi.Models;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHelperApi.Controllers
{
    [ApiController]
    [Route("v1/friendRequest")]
    public class FriendRequestController : ControllerBase
    {
        private readonly FriendRequestService _friendRequestService;
        private readonly FriendListService _friendListService;

        public FriendRequestController(FriendRequestService friendRequestService, FriendListService friendListService)
        {
            _friendRequestService = friendRequestService;
            _friendListService = friendListService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<FriendRequest>> Create(FriendRequest friendRequest)
        {
            if (!ModelState.IsValid)
                return Problem(statusCode: 400, title: "Algum dado inserido está em um formato incorreto");

            await _friendRequestService.InsertAsync(friendRequest);
            return CreatedAtAction(nameof(Create), new { id = friendRequest.Id }, friendRequest);

            #region GetFriendRequestResponse
            //if (friendRequest.Status == true)
            //{
            //    var friendList = new FriendList();
            //    friendList.UserFriendId = friendRequest.UserIdReceveidSolicitation;
            //    friendList.UserId = friendRequest.UserIdSolicitation;
            //    // se o status do friendRequest for true, então adicionar o UserIdSolicitation e UserIdReceveidSolicitation na tabela FriendList
            //    //await _friendRequestService.InsertAsync(friendRequest);
            //    await _friendListService.InsertAsync(friendList);
            //    return CreatedAtAction(nameof(Create), new { id = friendRequest.Id }, friendRequest);
            //}
            //else
            //{
            //    await _friendRequestService.RemoveAsync(friendRequest.Id);//return notfound ou bad request
            //    return BadRequest();
            //}
            //return friendRequest;
            #endregion
        }
            
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<FriendRequest>>> GetAll()
        {
            var friendRequests = await _friendRequestService.FindAllAsync();
            return Ok(friendRequests);
        }
    }
}

