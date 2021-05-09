using System.ComponentModel.DataAnnotations;

namespace FindHelperApi.Models
{
    public class FriendList
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int UserFriendId  { get; set; }
    }
}
