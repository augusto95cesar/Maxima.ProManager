using ProManager.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProManager.Domain.Entity
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string UserSystem { get; set; }
        public string SenhaSystem { get; set; } 
        public TypeUserSystem TypeUserSystem { get; set; }
    }
}
