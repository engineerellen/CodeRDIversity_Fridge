using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? SenhaSalt { get; set; }
        public string? SenhaHash { get; set; }
    }
}