using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CDC.Dominio.Models
{

    [MetadataType(typeof(TblProduto))]
    public partial class Produto { }

    public class TblProduto
    {
        [Required(ErrorMessage = "Campo obrigátorio")]
        [DisplayName("Descrição")]
        [AllowHtml]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio")]
        public string Imagem { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio")]
        [Remote("Disponibilidade", "Produto", AdditionalFields = "IdProduto", ErrorMessage = "Nome em uso.")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio")]
        public double Peso { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio")]
        [DisplayName("Preço")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio")]
        public List<TipodeProduto> Tipo { get; set; }
    }
}