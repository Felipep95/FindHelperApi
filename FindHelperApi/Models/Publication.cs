using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FindHelperApi.Models
{
    public class Publication
    {
        //[Key]
        public int Id { get; set; }
        
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O Campo Descrição é obrigatório.")]
        [StringLength(500, ErrorMessage = "O campo descrição aceita no máximo {0} caracteres")]
        public string Description { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "O Campo Data é obrigatório.")]
        public DateTime Date { get; set; }

        //[JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        //[Display(Name = "Foto")]
        //public string Photo { get; set; }
    }
}
