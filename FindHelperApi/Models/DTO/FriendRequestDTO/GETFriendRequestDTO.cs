using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindHelperApi.Models.DTO.FriendRequestDTO
{
    public class GETFriendRequestDTO
    {
        public int Id { get; set; }

        public int UserIdSolicitation { get; set; } 

        public int UserIdReceveidSolicitation { get; set; } 

        public bool Status { get; set; }

        public bool IsFriend { get; set; }
    }
}
