using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FindHelperApi.Models
{
    public class FriendList
    {
        //[Key]
        public int Id { get; set; }
        public int UserId { get; set; }//usuário que enviou a solicitação de amizade
        public int UserFriendId  { get; set; }//usuário que aceitou a solicitação de amizade

        [JsonIgnore]
        public User User { get; set; }
    }
}
