using System.ComponentModel.DataAnnotations;

namespace FilmesWPF.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Senha { get; set; }

        [StringLength(100)]
        public string Nome { get; set; }
    }
}
