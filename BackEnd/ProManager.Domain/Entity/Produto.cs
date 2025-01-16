using ProManager.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ProManager.Domain.Entity
{
    public class Produto
    {
        [Key]
        [Required]
        [StringLength(50)]
        //ID - Identificador do Produto - UUID
        public Guid Id { get; set; }

        //Código - Código apresentável ao usuário - Texto
        public string Codigo { get; set; }

        //Descrição - Descrição do Produto - Texto
        public string Descricao { get; set; }

        //Departamento - Lista de departamentos - Caixa de Seleção(Será consumido via GET da api criada)
        public string Departamento { get; set; }

        //Preço - Preço do Produto - Decimal
        public decimal Preco { get; set; }

        //Status - Ativo / Inativo - True/False - Booleano
        public bool Status { get; set; }

        //Ações - Editar / Excluir - A exclusão é lógica e não física
        public AcoesProduto Acoes { get; set; }
    }
}
