using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CDC.Dominio.Models
{
    
    public class OperacoesCarrinho
    {
        private List<ItemCarrinho> itens = new List<ItemCarrinho>();

        public void AdicionarItem (Produto produto, int quantidade)
        {
            ItemCarrinho item = itens.Where(p => p.Produto.IdProduto == produto.IdProduto).FirstOrDefault();

            if (item == null)
            {
                itens.Add(new ItemCarrinho
                {
                    Produto = produto,
                    Quantidade = quantidade
                });
            }// fim if

            else
            {
                item.Quantidade = item.Quantidade + quantidade;
            }// fil else
        }// fim adicionar

        public void RemoverItem(Produto produto)
        {
            itens.RemoveAll(p => p.Produto.IdProduto == produto.IdProduto);
        }// fim remover

        public decimal ValorTotal ()
        {
            return itens.Sum(p => p.Produto.Preco * p.Quantidade);
        }

        public void Clear()
        {
            itens.Clear();
        }

        public IEnumerable<ItemCarrinho> Linhas()
        {
            return itens;
        }
    }
}
