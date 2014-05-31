using CDC.Dominio.Models;
using System;
using System.Collections.Generic;
using CDC.Dominio.ViewModels;

namespace CDC.DAL.Interface
{
    public interface IProdutoRepositorio : IDisposable
    {
        IEnumerable<Produto> PegarTodos();

        IEnumerable<string> PegarTiposOrdenado();

        IEnumerable<Produto> PegarTodosPaginado(string categoria, int pagina = 1);

        Produto PegarPorId(int? id);

        void Criar(Produto produto);

        void Atualizar(Produto produto);

        void Excluir(Produto produto);

        IEnumerable<Produto> Procurar(string nome);
    }
}
