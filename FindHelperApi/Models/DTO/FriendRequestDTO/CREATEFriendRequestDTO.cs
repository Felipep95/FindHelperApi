namespace FindHelperApi.Models.DTO.FriendRequestDTO
{
    public class CREATEFriendRequestDTO
    {
        public int UserIdSolicitation { get; set; } 

        public int UserIdReceveidSolicitation { get; set; }

        public bool IsFriend { get; set; }
    }
}
