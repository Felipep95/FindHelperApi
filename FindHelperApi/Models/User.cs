using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FindHelperApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Números e caracteres especiais não são permitidos no nome.")]//corrigir regex para permitir caracteres especiais
        public string Name { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        [Required(ErrorMessage = "O Campo Email é obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O Email deve ter no mínimo {1} e no máximo {0} caracteres.")]
        public string Email { get; set; }

        //[JsonIgnore]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O Campo Senha é obrigatório.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha precisa ter no mínimo {2} caracteres.")]
        public string Password { get; set; }

        //[Display(Name = "Confirmar senha")]
        //[DataType(DataType.Password)]
        //[Required(ErrorMessage = "O Campo confirmar senha é obrigatório.")]
        //[Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        //public string ConfirmPassword { get; set; }
    }
}
