using System.ComponentModel.DataAnnotations;

namespace FilmesWPF.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do filme é obrigatório.")]
        [StringLength(120)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        [StringLength(60)]
        public string Genero { get; set; }

        public int Ano { get; set; }

        [StringLength(80)]
        public string Diretor { get; set; }

        public int DuracaoMin { get; set; }

        public double Nota { get; set; }
    }
}
