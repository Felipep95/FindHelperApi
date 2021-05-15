using FindHelperApi.Models;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FindHelperApi.Controllers
{
    [Route("api/friendList")]
    [ApiController]
    public class FriendListController : ControllerBase
    {
        private readonly FriendListService _friendListService;

        public FriendListController(FriendListService friendListService)
        {
            _friendListService = friendListService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FriendRequest>> Create(FriendRequest friendRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (friendRequest.Status == true)
            {
                var friendList = new FriendList();
                friendList.UserFriendId = friendRequest.UserIdReceveidSolicitation;
                friendList.UserId = friendRequest.UserIdSolicitation;
                await _friendListService.InsertAsync(friendList);
            }
            else
            {
                return Problem(statusCode: 400, title:"solicitação de amizade recusada");
            }
           
            return CreatedAtAction(nameof(Create), new { id = friendRequest.Id,}, friendRequest);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<FriendList>> GetAll()
        {
            var friendLists = await _friendListService.FindAllAsync();
            return Ok(friendLists);
        }
    }
}
