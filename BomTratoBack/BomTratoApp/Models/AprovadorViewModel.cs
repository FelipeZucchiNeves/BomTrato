using System;
using System.ComponentModel.DataAnnotations;

namespace BomTratoApp.Models
{
    public class AprovadorViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Last Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The E-mail is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime BirthDate { get; set; }
    }
}
