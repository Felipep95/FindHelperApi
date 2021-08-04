using FindHelperApi.Models;
using FindHelperApi.Models.DTO.FriendRequestDTO;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHelperApi.Controllers
{
    [ApiController]
    [Route("friendRequest")]
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
        public async Task<ActionResult <GETFriendRequestDTO>> Create(CREATEFriendRequestDTO friendRequestDTO)
        {
            if (!ModelState.IsValid)
                return Problem(statusCode: 400, title: "Formato de dado inválido");

            var createdFriendRequest = await _friendRequestService.InsertAsync(friendRequestDTO);
            return CreatedAtAction(nameof(Create), new { id = createdFriendRequest.Id }, createdFriendRequest);

            #region GetFriendRequestResponse
            
            #endregion
        }
            
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<FriendRequest>>> GetAll()
        {
            var friendRequests = await _friendRequestService.FindAllAsync();
            return Ok(friendRequests);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<GETFriendRequestDTO>> Update(CREATEFriendRequestDTO friendRequestDTO)
        {
            if (!ModelState.IsValid)
                return Problem(statusCode: 400, title: "Formato de dado inválido");

            var updatedFriendRequest = await _friendRequestService.FriendRequestResponse(friendRequestDTO);
            return CreatedAtAction(nameof(Create), new { id = updatedFriendRequest.Id }, updatedFriendRequest);
        }
    }
}

