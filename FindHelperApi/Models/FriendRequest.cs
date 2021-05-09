using System.ComponentModel.DataAnnotations;

namespace FindHelperApi.Models
{
    public class FriendRequest
    {
        [Key]
        public int Id { get; set; }
        
        public int UserIdSolicitation { get; set; } //ID do Usuário logado no APP
        public User User { get; set; }
        
        public int UserIdReceveidSolicitation { get; set; } // ID do Usuário que recebeu a solicitação de amizade
        
        public bool Status { get; set; } // Indica se o usuário aceitou ou não solicitação
    }
}
