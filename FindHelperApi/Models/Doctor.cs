using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FindHelperApi.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Campo nome é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Números e caracteres especiais não são permitidos no nome.")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        [Required(ErrorMessage = "O Campo Email é obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O Email deve ter no mínimo {1} e no máximo {0} caracteres.")]
        public string Email { get; set; }
        
        [Display(Name = "Experiência")]
        [Required(ErrorMessage = "O Campo Experiência é obrigatório")]
        public string Experience { get; set; }
        
        [Display(Name = "CRM")]
        [Required(ErrorMessage = "O Campo CRM é obrigatório")]
        public int CRM { get; set; }
    }
}
