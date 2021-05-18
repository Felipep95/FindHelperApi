using System.ComponentModel.DataAnnotations;

namespace FindHelperApi.Models.DTO
{
    public class CREATEAreaDTO
    {
        [Required(ErrorMessage = "O Campo nome é obrigatório.")]
        public string Name { get; set; }
    }
}
