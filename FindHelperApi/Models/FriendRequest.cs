using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindHelperApi.Models
{
    public class FriendRequest
    {
        public int UserIdSolicitation { get; set; } //ID do Usuário logado no APP
        public int UserIdReceveidSolicitation { get; set; } // ID do Usuário que recebeu a solicitação de amizade
        public bool Status { get; set; } // Indica se o usuário aceitou ou não solicitação
    }
}
