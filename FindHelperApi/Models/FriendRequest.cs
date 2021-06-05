using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FindHelperApi.Models
{
    public class FriendRequest
    {
        //[Key]
        public int Id { get; set; }
        
        public int UserIdSolicitation { get; set; } //ID do Usuário logado no APP
        
        public int UserIdReceveidSolicitation { get; set; } // ID do Usuário que recebeu a solicitação de amizade

        //[JsonIgnore]
        //public User User { get; set; }

        public bool Status { get; set; } // Indica se o usuário aceitou ou não a solicitação

        public bool isFriend { get; set; }
    }
}
