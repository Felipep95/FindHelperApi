using System;
using System.ComponentModel.DataAnnotations;

namespace FindHelperApi.Models
{
    public class Publication
    {
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O Campo Descrição é obrigatório.")]
        [StringLength(500, ErrorMessage = "O campo descrição aceita no máximo {0} caracteres")]
        public string Description { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "O Campo Data é obrigatório.")]
        public DateTime Date { get; set; }

        //[Display(Name = "Foto")]
        //public string Photo { get; set; }
    }
}
