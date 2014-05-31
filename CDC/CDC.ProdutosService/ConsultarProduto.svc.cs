using System.Collections.Generic;
using CDC.DAL.Interface;
using CDC.Dominio.Models;

namespace CDC.ProdutosService
{
    public class ConsultarProduto : IConsultarProduto
    {
        private IProdutoRepositorio _repositorio;

        public ConsultarProduto(){ }

        public ConsultarProduto(IProdutoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IEnumerable<Produto> Consultar(string nome)
        {
            var produto = _repositorio.Procurar(nome);
            return produto;
        }
    }
}