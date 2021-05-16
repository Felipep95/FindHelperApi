using System.ComponentModel.DataAnnotations;

namespace FindHelperApi.Models.DTO
{
    public class LOGINUserDTO
    {

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        [Required(ErrorMessage = "O Campo Email é obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O Email deve ter no mínimo {1} e no máximo {0} caracteres.")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O Campo Senha é obrigatório.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha precisa ter no mínimo {2} caracteres.")]
        public string Password { get; set; }
    }
}
