namespace FindHelperApi.Models
{
    public class FriendList
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int UserFriendId  { get; set; }
    }
}
