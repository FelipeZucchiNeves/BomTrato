using System;
using System.ComponentModel.DataAnnotations;

namespace BomTratoApp.Models
{
    public class EscritorioViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Rua é requerido")]
        [MinLength(2)]
        [MaxLength(50)]
        public string Street { get; set; }
        [Required(ErrorMessage = "Número é requerido")]
        [MinLength(2)]
        [MaxLength(10)]
        public string Number { get; set; }
        [Required(ErrorMessage = "Estado é requerido")]
        [MinLength(2)]
        [MaxLength(4)]
        public string State { get; set; }
        [Required(ErrorMessage = "CEP é requerido")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "CEP inválido")]
        public int Cep { get; set; }
        [Required(ErrorMessage = "Cidade é requerida")]
        [MinLength(2)]
        [MaxLength(50)]
        public string City { get; set; }
        [Required(ErrorMessage = "Bairro é requerido")]
        [MinLength(2)]
        [MaxLength(50)]
        public string District { get; set; }
    }
}
