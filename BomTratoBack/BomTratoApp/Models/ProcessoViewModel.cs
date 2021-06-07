using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BomTratoApp.Models
{
    public class ProcessoViewModel
    {
        [Key]
        public string Id { get; set; }
        [StringLength(12)]
        [Required(ErrorMessage = "O número do processo deve ter exatamente 12 dígitos.")]
        public string ProcessNumber { get; set; }
        [Required(ErrorMessage = "O valor deve ser superior a R$ 30.000,00.")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "O valor não pode ter mais de duas casas decimais.")]
        [Range(30000, double.PositiveInfinity)]
        [Column(TypeName = "decimal(14,2)")]
        public decimal Value { get; set; }
        [Required]
        public Guid AprovadorId { get; set; }
        [Required(ErrorMessage = "Escritório é requerido")]
        public Guid EscritorioId { get; set; }
        [Required(ErrorMessage = "O processo deve estar aprovado ou em análise")]
        public bool Aproved { get; set; }
        [Required(ErrorMessage = "O processo deve estar ativo ou inativo")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "O nome do cliente é requerido")]
        [MinLength(2)]
        [MaxLength(100)]
        public string ComplainerName { get; set; }
    }
}
