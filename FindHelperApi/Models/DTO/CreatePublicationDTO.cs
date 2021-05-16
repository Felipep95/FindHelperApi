using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FindHelperApi.Models.DTO
{
    public class CreatePublicationDTO
    {
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O Campo Descrição é obrigatório.")]
        [StringLength(500, ErrorMessage = "O campo descrição aceita no máximo {0} caracteres")]
        public string description { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "O Campo Data é obrigatório.")]
        public DateTime date { get; set; }

        public int userId { get; set; } 
    }
}
