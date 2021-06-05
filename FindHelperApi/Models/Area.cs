using System.ComponentModel.DataAnnotations;

namespace FindHelperApi.Models
{
    public class Area
    {
        //[Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Campo nome é obrigatório.")]
        public string Name { get; set; }
    }
}
