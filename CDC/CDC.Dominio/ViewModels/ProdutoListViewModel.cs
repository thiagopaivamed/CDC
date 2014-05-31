using System.Collections;
using CDC.Dominio.Models;
using System.Collections.Generic;

namespace CDC.Dominio.ViewModels
{
    public class ProdutoListViewModel 
    {
        public IEnumerable<Produto> Produtos { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string Categoria { get; set; }
    }
}